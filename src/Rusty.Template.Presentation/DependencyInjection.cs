﻿#region

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
using Rusty.Template.Application.Services;
using Rusty.Template.Contracts.Dtos.User;
using Rusty.Template.Contracts.Requests.Authentication;
using Rusty.Template.Domain.Exceptions.Domain;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Mapping;
using Rusty.Template.Infrastructure.Options;
using Rusty.Template.Infrastructure.Repositories.Specific;
using Rusty.Template.Infrastructure.Services;
using Rusty.Template.Presentation.OperationFilters;
using Rusty.Template.Presentation.SchemaFilters;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Filters;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

#endregion

namespace Rusty.Template.Presentation;

internal static class DependencyInjection
{
	internal static ConfigureHostBuilder AddSerilog(this ConfigureHostBuilder host)
	{
		host.UseSerilog((ctx, lc) =>
			lc.ReadFrom.Configuration(ctx.Configuration));

		return host;
	}
	internal static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddOptions();
		services.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));

		return services;
	}
	internal static IServiceCollection AddApiVersioningSupport(this IServiceCollection services, IConfiguration configuration)
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

		return services;
	}
	internal static IServiceCollection AddFluentValidation(this IServiceCollection services)
	{
		ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
		ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Continue;
		services.AddFluentValidationAutoValidation(opt =>
			opt.DisableDataAnnotationsValidation = true);
		services.AddValidatorsFromAssemblyContaining<UserCreateDtoValidator>();

		return services;
	}
	internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
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
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = authOptions.ValidateIssuer,
					ValidIssuer = authOptions.Issuer,
					ValidateAudience = authOptions.ValidateAudience,
					ValidAudience = authOptions.Audience,
					ValidateLifetime = authOptions.ValidateAccessTokenLifetime,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.Secret)),
					ValidateIssuerSigningKey = authOptions.ValidateSecret
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
							await context.Response.WriteAsJsonAsync(
								new SecurityTokenExpiredException("Token expired"));
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

		return services;
	}
	internal static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
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
				.Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location)!,
					$"{a.Name}.xml"))
				.Where(File.Exists).ToArray();
			Array.ForEach(xmlDocs, d => { options.IncludeXmlCommentsWithRemarks(d); });
			options.AddEnumsWithValuesFixFilters(o =>
			{
				// add schema filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema
				o.ApplySchemaFilter = true;
				// alias for replacing 'x-enumNames' in swagger document
				o.XEnumNamesAlias = "x-enum-varnames";

				// alias for replacing 'x-enumDescriptions' in swagger document
				o.XEnumDescriptionsAlias = "x-enum-descriptions";

				// add parameter filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema parameters
				o.ApplyParameterFilter = true;

				// add document filter to fix enums displaying in swagger document
				o.ApplyDocumentFilter = true;

				// add descriptions from DescriptionAttribute or xml-comments to fix enums (add 'x-enumDescriptions' or its alias from XEnumDescriptionsAlias for schema extensions) for applied filters
				o.IncludeDescriptions = true;

				// add remarks for descriptions from xml-comments
				o.IncludeXEnumRemarks = true;

				// get descriptions from DescriptionAttribute then from xml-comments
				o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;

				// new line for enum values descriptions
				o.NewLine = "\n";

				// get descriptions from xml-file comments on the specified path
				// should use "options.IncludeXmlComments(xmlFilePath);" before
				Array.ForEach(xmlDocs, d => { o.IncludeXmlCommentsFrom(d); });
			});
			// Enable openApi Annotations
			options.EnableAnnotations(true, true);
			options.DocumentFilter<TagOrderByNameDocumentFilter>();
			options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
			options.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
			options.OperationFilter<OperationIdFilter>();
			options.OperationFilter<ValidationOperationFilter>();
			options.OperationFilter<SecurityRequirementsOperationFilter>();
			options.SupportNonNullableReferenceTypes(); // Sets Nullable flags appropriately.              
			options.UseAllOfForInheritance(); // Allows $ref objects to be nullable
		});
		services.AddFluentValidationRulesToSwagger(options =>
		{
			options.SetNotNullableIfMinLengthGreaterThenZero = true;
		});
		services.AddRouting(options => options.LowercaseUrls = true);

		return services;
	}
	internal static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration,
													IWebHostEnvironment env)
	{
		services.AddDbContext<AppDbContext>(options =>
		{
			var efConStr = configuration.GetConnectionString("DefaultConnection") ??
						   throw new ConnectionStringIsNotValidException();
			var contextOptions = options.UseSqlServer(efConStr)
				.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			if (env.IsDevelopment())
				contextOptions.EnableSensitiveDataLogging().EnableDetailedErrors();
		});

		return services;
	}
	internal static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IUserRepo, UserRepo>();
		services.AddScoped<IGroupRepo, GroupRepo>();
		services.AddScoped<IRoleRepo, RoleRepo>();

		return services;
	}
	internal static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<IAuthenticationService, AuthenticationService>();
		return services;
	}
	internal static IServiceCollection AddMapster(this IServiceCollection services)
	{
		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(typeof(UserProfile).Assembly);
		services.AddSingleton(config);
		services.AddScoped<IMapper, ServiceMapper>();

		return services;
	}
}