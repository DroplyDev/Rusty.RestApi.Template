namespace Rusty.Template.Tests.Integration.V1.WeatherForecastController;

public sealed class PaginateWeatherForecastTests : BaseTest
{
    [Fact]
    public async Task Paginate_ReturnsOK_WhenOk()
    {
        var response = await Client.PagedAsync(new OrderedPagedRequest
        {
            OrderByData = new OrderByData
            {
                OrderBy = "Temperature",
                OrderDirection = (OrderDirection)3
            },
            PageData = new PageData
            {
                Offset = 0,
                Limit = 100
            }
        });
        response.Data.Should().NotBeEmpty();
        response.Data.Count.Should().Be(100);
    }

    public PaginateWeatherForecastTests(WebApiFactory apiFactory) : base(apiFactory)
    {
    }
}