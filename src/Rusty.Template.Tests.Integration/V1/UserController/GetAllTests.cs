namespace Rusty.Template.Tests.Integration.V1.UserController;

public class GetAllTests : BaseUserTests
{
	public GetAllTests(WebApiFactory apiFactory) : base(apiFactory)
	{
	}

	[Fact]
	public async Task GetAllAsync_Returns_OK_When_OK()
	{
		//Arrange

		//Act
		var response = await Client.GetAllUsersAsync();
		//Assert
		response.Count.Should().Be(100);
	}
}