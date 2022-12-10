namespace Rusty.Template.Contracts.Dtos.WeatherForecast;

public sealed record WeatherForecastDto
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF { get; set; }

    public string? Summary { get; set; }
}