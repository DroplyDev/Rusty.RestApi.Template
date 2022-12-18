namespace Rusty.Template.Tests.Integration.V1.WeatherForecastController;

public sealed class PaginateWeatherForecastTests : BaseTest
{
    public PaginateWeatherForecastTests(WebApiFactory apiFactory) : base(apiFactory)
    {
    }

    [Fact]
    public async Task Paginate_ReturnsOK_WhenOk()
    {
        var response = await Client.PagedAsync(new OrderedPagedRequest
        {
            OrderByData = new OrderByData
            {
                OrderBy = "TemperatureC",
                OrderDirection = OrderDirection._0
            },
            PageData = new PageData
            {
                Offset = 0,
                Limit = 100
            }
        });
        response.Data.Count.Should().Be(100);
    }
}