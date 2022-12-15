using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.WeatherForecast;

/// <summary>
///     The weather forecast create dto
/// </summary>
public sealed record WeatherForecastCreateDto
{
    /// <summary>
    ///     Gets or sets the value of the date
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     Gets or sets the value of the temperature c
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    ///     Gets or sets the value of the summary
    /// </summary>
    public string? Summary { get; set; }
}

/// <summary>
///     The weather forecast create dto validator class
/// </summary>
/// <seealso cref="AbstractValidator{WeatherForecastCreateDto}" />
public sealed class WeatherForecastCreateDtoValidator : AbstractValidator<WeatherForecastCreateDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WeatherForecastCreateDtoValidator" /> class
    /// </summary>
    public WeatherForecastCreateDtoValidator()
    {
        RuleFor(w => w.TemperatureC).GreaterThanOrEqualTo(500);
    }
}