using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.WeatherForecast;

/// <summary>
///     The weather forecast update dto
/// </summary>
public sealed record WeatherForecastUpdateDto
{
    /// <summary>
    ///     Gets or sets the value of the id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the value of the summary
    /// </summary>
    public string? Summary { get; set; }
}

/// <summary>
///     The weather forecast update dto validator class
/// </summary>
/// <seealso cref="AbstractValidator{WeatherForecastUpdateDto}" />
public sealed class WeatherForecastUpdateDtoValidator : AbstractValidator<WeatherForecastUpdateDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WeatherForecastUpdateDtoValidator" /> class
    /// </summary>
    public WeatherForecastUpdateDtoValidator()
    {
        RuleFor(w => w.Summary).MaximumLength(100).MinimumLength(3);
    }
}