using Rusty.Template.Application.Repositories;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Repositories.BaseRepository;

namespace Rusty.Template.Infrastructure.Repositories;

public class WeatherForecastRepo : BaseRepo<WeatherForecast>, IWeatherForecastRepo
{
    public WeatherForecastRepo(AppDbContext context) : base(context, item => item.Date)
    {
    }
}