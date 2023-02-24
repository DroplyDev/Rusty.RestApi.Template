using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;

namespace Rusty.Template.Tests.Integration.V1.AuthenticationController;
public class Login : BaseTests
{
	public Login(WebApiFactory apiFactory) : base(apiFactory)
	{
	}

	[Fact]
	public async Task Login_Returns_Ok_When_Ok()
	{
		//var appDbContext = ApiFactory.Services.GetRequiredService<AppDbContext>();
		////Arrange
		//appDbContext.Users.Add(new User
		//{
		//	UserName = "TestUser",
		//	Email = "TestUser@gmail.com",
		//	Password = "Test"

		//});
		var request = new LoginRequest
		{
			Username = "TestUser",
			Password = "Test"
		};
		//Act
		var response = await Client.LoginAsync(request);
		//Assert
		response.Should().NotBeNull().Should().Be(200);
	}
}