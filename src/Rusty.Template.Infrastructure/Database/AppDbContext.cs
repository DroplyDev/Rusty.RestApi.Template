using Microsoft.EntityFrameworkCore;
using Rusty.Template.Domain;

namespace Rusty.Template.Infrastructure.Database;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
}