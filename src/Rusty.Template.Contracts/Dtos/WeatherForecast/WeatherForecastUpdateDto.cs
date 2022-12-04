namespace Rusty.Template.Contracts.Dtos.WeatherForecast;

public record WeatherForecastUpdateDto
{
    public int Id { get; set; }
    public string? Summary { get; set; }
}