namespace Rusty.Template.Tests.Integration.V1.UserController;

public class GetByIdTests : BaseUserTests
{
	public GetByIdTests(WebApiFactory apiFactory) : base(apiFactory)
	{
	}

	[Fact]
	public async Task GetUserByIdAsync_Returns_Ok_When_Ok()
	{
		//Arrange
		const int request = 1;
		//Act
		var response = await Client.GetUserByIdAsync(request);
		//Assert
		// response.UserName.Should().Be("Test");
	}
}