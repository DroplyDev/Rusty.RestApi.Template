using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Rusty.Template.Infrastructure.Database;

/// <summary>
///     The db initializer class
/// </summary>
public static class DbInitializer
{
    /// <summary>
    ///     Initializes the database data using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    public static async Task InitializeDatabaseDataAsync(this IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        // var weatherForecastRepo = scope.ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
        // if (await weatherForecastRepo.IsEmptyAsync())
        // {
        //     var weatherForecasts = new List<WeatherForecast>();
        //     var rnd = new Random();
        //     for (var i = 0; i < 10000; i++)
        //         weatherForecasts.Add(new WeatherForecast
        //         {
        //             Date = DateTime.Now.AddDays(i * -1),
        //             TemperatureC = rnd.Next(-20, 50),
        //             Summary = LoremIpsum(10, 20, 1, 3, 1)
        //         });
        //     await weatherForecastRepo.CreateRangeAsync(weatherForecasts);
        // }
    }

    /// <summary>
    ///     Migrates the database using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    public static async Task MigrateDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
    }

    /// <summary>
    ///     Creates the database from context if not exists using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    public static async Task CreateDatabaseFromContextIfNotExistsAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();
    }

    /// <summary>
    ///     Lorems the ipsum using the specified min words
    /// </summary>
    /// <param name="minWords">The min words</param>
    /// <param name="maxWords">The max words</param>
    /// <param name="minSentences">The min sentences</param>
    /// <param name="maxSentences">The max sentences</param>
    /// <param name="numParagraphs">The num paragraphs</param>
    /// <returns>The string</returns>
    private static string LoremIpsum(int minWords, int maxWords,
        int minSentences, int maxSentences,
        int numParagraphs)
    {
        var words = new[]
        {
            "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
            "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
            "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
        };

        var rand = new Random();
        var numSentences = rand.Next(maxSentences - minSentences)
                           + minSentences + 1;
        var numWords = rand.Next(maxWords - minWords) + minWords + 1;

        var result = new StringBuilder();

        for (var p = 0; p < numParagraphs; p++)
        for (var s = 0; s < numSentences; s++)
        {
            for (var w = 0; w < numWords; w++)
            {
                if (w > 0) result.Append(" ");
                result.Append(words[rand.Next(words.Length)]);
            }

            result.Append(". ");
        }

        return result.ToString();
    }
}