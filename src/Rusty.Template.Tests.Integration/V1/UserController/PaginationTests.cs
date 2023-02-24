using Rusty.Template.Contracts.Requests.Pagination;

namespace Rusty.Template.Tests.Integration.V1.UserController;

public class PaginationTests : BaseTests
{
	public PaginationTests(WebApiFactory apiFactory) : base(apiFactory)
	{
	}

	[Fact]
	[TestPriority(2)]
	public async Task PagedAsync_Returns_Ok_When_Ok()
	{

		//Arrange
		var request = new FilterOrderPageRequest
		{
			OrderByData = new OrderByData
			{
				OrderDirection = OrderDirection.Asc,
				OrderBy = "Username"
			},
			PageData = new PageData
			{
				Offset = 100,
				Limit = 100
			}
		};
		//Act
		var response = await Client.GetFilteredPagedUsersAsync(request);
		//Assert
		response.StatusCode.Should().Be(200);
		response.Result.TotalCount.Should().Be(100);
	}

}