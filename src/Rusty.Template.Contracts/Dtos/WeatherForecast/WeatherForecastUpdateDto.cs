using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.WeatherForecast;

public sealed record WeatherForecastUpdateDto
{
    public int Id { get; set; }
    public string? Summary { get; set; }
}

public sealed class WeatherForecastUpdateDtoValidator : AbstractValidator<WeatherForecastUpdateDto>
{
    public WeatherForecastUpdateDtoValidator()
    {
        RuleFor(w => w.Summary).MaximumLength(100).MinimumLength(3);
    }
}