using Rusty.Template.Application.Repositories;
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
}