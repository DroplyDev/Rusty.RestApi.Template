namespace Rusty.Template.Contracts.Dtos.WeatherForecast;

/// <summary>
///     The weather forecast dto
/// </summary>
public sealed record WeatherForecastDto
{
    /// <summary>
    ///     Gets or sets the value of the id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the value of the date
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     Gets or sets the value of the temperature c
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    ///     Gets or sets the value of the temperature f
    /// </summary>
    public int TemperatureF { get; set; }

    /// <summary>
    ///     Gets or sets the value of the summary
    /// </summary>
    public string? Summary { get; set; }
}