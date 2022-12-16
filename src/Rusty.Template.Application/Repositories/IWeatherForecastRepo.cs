using Rusty.Template.Domain;

namespace Rusty.Template.Application.Repositories;

/// <summary>
///     The weather forecast repo interface
/// </summary>
/// <seealso cref="IBaseRepo{WeatherForecast}" />
public interface IWeatherForecastRepo : IBaseRepo<WeatherForecast>
{
}