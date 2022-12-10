using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
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

public static class ServiceInitializer
{
    public static void AddSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((ctx, lc) =>
            lc.ReadFrom.Configuration(ctx.Configuration));
    }

    public static void AddConfigurations(this ConfigurationManager configuration)
    {
        configuration.AddJsonFile("appsettings.json", false, true);
        configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            true);
        configuration.AddEnvironmentVariables();
    }

    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddLogging();
        services.AddDatabases(configuration);
        services.AddFluentValidation();
        services.AddControllers();
        services.AddSwagger();
        services.AddRepositories();
        services.AddServices();
        services.AddMapster();
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });
    }

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<WeatherForecastCreateDtoValidator>();
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TestMicroservice",
                Version = "v1"
            });
            c.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "TestMicroservice",
                Version = "v2"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            //c.OperationFilter<SwaggerAppendAuthorizeToSummaryOperationFilter>();
        });
    }

    private static void AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var efConStr = configuration.GetConnectionString("DefaultConnection");
            if (efConStr is null)
                throw new ApiException("Connection string not found");
            options.UseSqlServer(efConStr);
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWeatherForecastRepo, WeatherForecastRepo>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        
    }

    private static void AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(WeatherForecastProfile).Assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}