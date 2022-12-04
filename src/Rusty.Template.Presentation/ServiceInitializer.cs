using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Exceptions;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Profiles;
using Rusty.Template.Infrastructure.Repositories;
using Serilog;

namespace Rusty.Template.Presentation;

public static class ServiceInitializer
{
    public static ConfigureHostBuilder AddSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((ctx, lc) =>
            lc.ReadFrom.Configuration(ctx.Configuration));
        return host;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddLogging();
        services.AddDatabases(configuration);
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddRepositories();
        services.AddServices();
        services.AddMapster();
        return services;
    }

    private static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        var efConStr = configuration.GetConnectionString("DefaultConnection");
        if (efConStr is null)
            throw new ApiException("Connection string not found");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(efConStr));
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWeatherForecastRepo, WeatherForecastRepo>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(WeatherForecastProfile).Assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}