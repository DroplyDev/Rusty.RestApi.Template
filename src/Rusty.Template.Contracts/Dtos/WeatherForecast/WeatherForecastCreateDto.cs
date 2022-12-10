using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.WeatherForecast;

public sealed record WeatherForecastCreateDto
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}

public sealed class WeatherForecastCreateDtoValidator : AbstractValidator<WeatherForecastCreateDto>
{
    public WeatherForecastCreateDtoValidator()
    {
        RuleFor(w => w.TemperatureC).GreaterThanOrEqualTo(500);
    }
}