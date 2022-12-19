using System.Globalization;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Exceptions.Entity;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Repositories.BaseRepository;

namespace Rusty.Template.Infrastructure.Repositories;

/// <summary>
///     The weather forecast repo class
/// </summary>
/// <seealso cref="BaseRepo{WeatherForecast}" />
/// <seealso cref="IWeatherForecastRepo" />
public class WeatherForecastRepo : BaseRepo<WeatherForecast>, IWeatherForecastRepo
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WeatherForecastRepo" /> class
    /// </summary>
    /// <param name="context">The context</param>
    public WeatherForecastRepo(AppDbContext context) : base(context, item => item.Date)
    {

  
    }

    /// <summary>
    ///     Creates the no save using the specified entity
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <exception cref="EntityWitNameAlreadyExistsException{WeatherForecast}"></exception>
    public override async Task CreateNoSaveAsync(WeatherForecast entity)
    {
        if (await ExistsAsync(item => item.Date == entity.Date))
            throw new EntityWitNameAlreadyExistsException<WeatherForecast>(
                entity.Date.ToString(CultureInfo.CurrentCulture));
        await base.CreateNoSaveAsync(entity);
    }
}