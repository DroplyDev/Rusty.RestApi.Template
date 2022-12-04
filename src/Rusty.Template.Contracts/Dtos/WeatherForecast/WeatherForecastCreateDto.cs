namespace Rusty.Template.Contracts.Dtos.WeatherForecast;

public record WeatherForecastCreateDto
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}