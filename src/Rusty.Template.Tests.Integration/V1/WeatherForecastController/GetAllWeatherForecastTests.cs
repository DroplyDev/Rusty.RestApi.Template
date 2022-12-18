namespace Rusty.Template.Tests.Integration.V1.WeatherForecastController;

public sealed class GetAllWeatherForecastTests : BaseTest
{
    [Fact]
    public async Task GetAll_ReturnsOK_WhenOk()
    {
        
        var response = await Client.PagedAsync(new OrderByPagedRequest
        {
            // OrderByData = null,
            PageData = new PageData
            {
                Offset = 0,
                Limit = 100
            }
        });
        response.Data.Should().NotBeEmpty();
        response.Data.Count.Should().Be(100);
    }
    
    public GetAllWeatherForecastTests(WebApiFactory apiFactory) : base(apiFactory)
    {
    }
}