using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Dtos.WeatherForecast;
using Rusty.Template.Contracts.Exceptions;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Mapping;
using Rusty.Template.Infrastructure.Repositories;
using Serilog;

namespace Rusty.Template.Presentation;

/// <summary>
///     The service initializer class
/// </summary>
public static class ServiceInitializer
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
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<WeatherForecastCreateDtoValidator>();
    }

    /// <summary>
    ///     Adds the swagger using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    /// <param name="configuration">The configuration</param>
    public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenConfigurationOptions>();
        services.AddSwaggerGen(options =>
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            var swaggerSection = configuration.GetSection("Swagger");
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = swaggerSection["Title"],
                        Description = swaggerSection["Description"] + (description.IsDeprecated ? " [DEPRECATED]" : string.Empty),
                        Version = description.ApiVersion.ToString()
                    });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field to get access to secured methods",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
            var currentAssembly = Assembly.GetExecutingAssembly();
            var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();

            Array.ForEach(xmlDocs, d => { options.IncludeXmlComments(d); });  
        });
    }

    /// <summary>
    ///     Adds the databases using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    /// <param name="configuration">The configuration</param>
    /// <exception cref="ApiException">Connection string was not found</exception>
    public static void AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var efConStr = configuration.GetConnectionString("DefaultConnection");
            if (efConStr is null)
                throw new ApiException("Connection string was not found");
            options.UseSqlServer(efConStr)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }

    /// <summary>
    ///     Adds the repositories using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWeatherForecastRepo, WeatherForecastRepo>();
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
        config.Scan(typeof(WeatherForecastProfile).Assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}