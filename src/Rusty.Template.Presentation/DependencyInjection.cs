using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Dtos.User;
using Rusty.Template.Contracts.Exceptions;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Mapping;
using Rusty.Template.Infrastructure.Repositories.AppDbRepo;
using Rusty.Template.Presentation.Options;
using Rusty.Template.Presentation.SchemaFilters;
using Serilog;

namespace Rusty.Template.Presentation;

/// <summary>
///     The service initializer class
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    ///     Adds the serilog using the specified host
    /// </summary>
    /// <param name="host">The host</param>
    public static void AddSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((ctx, lc) =>
            lc.ReadFrom.Configuration(ctx.Configuration));
    }

    /// <summary>
    ///     Adds the configurations using the specified configuration
    /// </summary>
    /// <param name="configuration">The configuration</param>
    public static void AddConfigurations(this ConfigurationManager configuration)
    {
        configuration.AddJsonFile("appsettings.json", false, true);
        configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            true);
        configuration.AddEnvironmentVariables();
    }

    /// <summary>
    ///     Adds the api versioning support using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    /// <param name="configuration">The configuration</param>
    public static void AddApiVersioningSupport(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(DateTime.Now);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.RegisterMiddleware = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VV";
            options.SubstituteApiVersionInUrl = true;
        });
    }

    /// <summary>
    ///     Adds the fluent validation using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(opt =>
            opt.DisableDataAnnotationsValidation = true);
        services.AddValidatorsFromAssemblyContaining<UserCreateDtoValdiatior>();
    }

    /// <summary>
    ///     Adds the configurations using the specified configuration
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">The configuration</param>
    public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>() ??
                          throw new NullReferenceException();
        services.AddCors(options =>
        {
            options.AddPolicy("All", builder =>
            {
                builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                // options.Authority = "https://example.com";
                // options.Audience = "api";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = authOptions.ValidateIssuer,
                    ValidIssuer = authOptions.Issuer,
                    ValidateAudience = authOptions.ValidateAudience,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = authOptions.ValidateAccessTokenLifetime,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.Key)),
                    ValidateIssuerSigningKey = authOptions.ValidateKey
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        if (context.AuthenticateFailure?.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            await context.Response.WriteAsJsonAsync(new SecurityTokenExpiredException());
                            return;
                        }

                        await context.Response.WriteAsync("Not authorized");
                    },
                    // OnForbidden = context => { },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("Token-Expired", "true");

                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });
    }

    /// <summary>
    ///     Adds the swagger using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    /// <param name="configuration">The configuration</param>
    public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            var swaggerSection = configuration.GetSection("Swagger");
            var licenseSection = swaggerSection.GetSection("License");
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = swaggerSection["Title"],
                        Description = swaggerSection["Description"] +
                                      (description.IsDeprecated ? " [DEPRECATED]" : string.Empty),
                        Version = description.ApiVersion.ToString(),
                        TermsOfService = string.IsNullOrEmpty(swaggerSection["TermsOfServiceUrl"])
                            ? null
                            : new Uri(swaggerSection["TermsOfServiceUrl"]!),
                        License = licenseSection is null
                            ? null
                            : new OpenApiLicense
                            {
                                Name = licenseSection["Name"],
                                Url = new Uri(licenseSection["Url"]!)
                            }
                    });
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put ONLY your JWT Bearer token in text box below!",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    jwtSecurityScheme,
                    Array.Empty<string>()
                }
            });
            var currentAssembly = Assembly.GetExecutingAssembly();
            var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location)!, $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();
            Array.ForEach(xmlDocs, d => { options.IncludeXmlComments(d); });

            options.SupportNonNullableReferenceTypes();

            options.OrderActionsBy(apiDesc =>
                $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}_{apiDesc.RelativePath}");

            options.SchemaFilter<EnumSchemaFilter>();
        });
        services.AddFluentValidationRulesToSwagger(options =>
        {
            options.SetNotNullableIfMinLengthGreaterThenZero = true;
        });

        services.AddRouting(options => options.LowercaseUrls = true);
    }

    /// <summary>
    ///     Adds the databases using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    /// <param name="configuration">The configuration</param>
    /// <exception cref="ApiException">Connection string was not found</exception>
    public static void AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        var efConStr = configuration.GetConnectionString("DefaultConnection");
        services.AddSingleton(new ConnectionStringFactory(efConStr));

        services.AddDbContextFactory<AppDbContext>(options =>
        {
            var factory = services.BuildServiceProvider().GetRequiredService<ConnectionStringFactory>();
            options.UseSqlServer(factory.ConnectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }

    /// <summary>
    ///     Adds the repositories using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IGroupRepo, GroupRepo>();
        services.AddScoped<IRoleRepo, RoleRepo>();
    }

    /// <summary>
    ///     Adds the services using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    public static void AddServices(this IServiceCollection services)
    {
    }

    /// <summary>
    ///     Adds the mapster using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    public static void AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(UserProfile).Assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}