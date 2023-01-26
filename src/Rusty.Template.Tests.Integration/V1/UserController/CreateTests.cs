#region

using Bogus;
using Microsoft.AspNetCore.Http;

#endregion

namespace Rusty.Template.Tests.Integration.V1.UserController;

public class CreateTests : BaseUserTests
{
	public CreateTests(WebApiFactory apiFactory) : base(apiFactory)
	{
	}

	[Fact]
	public async Task CreateUserAsync_Returns_Created_When_Ok()
	{
		Randomizer.Seed = new Random();

		//Arrange
		var request = MoqDataGenerator.UserCreateDto().First();
		//Act
		var response = await Client.CreateUserAsync(request);

		//Assert
		response.StatusCode.Should().Be(StatusCodes.Status201Created);
		response.Result.Id.Should().NotBe(0);
		// response.Result.UserName.Should().Be(request.UserName);
		response.Result.Email.Should().Be(request.Email);
	}

	[Fact]
	public async Task CreateUserAsync_Returns_BadRequest_When_ValidationFailed()
	{
		//Arrange
		var request = MoqDataGenerator.UserCreateDto().First();
		//Act
		var response = await Client.CreateUserAsync(request);
		//Assert
		response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
	}
}