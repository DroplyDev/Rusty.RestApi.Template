using Microsoft.EntityFrameworkCore;
using Rusty.Template.Domain;

namespace Rusty.Template.Infrastructure.Database;

/// <summary>
///     The app db context class
/// </summary>
/// <seealso cref="DbContext" />
public partial class AppDbContext : DbContext
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AppDbContext" /> class
    /// </summary>
    /// <param name="options">The options</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    ///     Gets or sets the value of the weather forecasts
    /// </summary>
    public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
}