#region

using Bogus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;
using System.Net.Http.Headers;
using System.Net.Http;

#endregion

namespace Rusty.Template.Tests.Integration;

public abstract class BaseTests : IClassFixture<WebApiFactory>
{
	protected readonly WebApiFactory ApiFactory;
	protected readonly NSwagClient Client;

	protected BaseTests(WebApiFactory apiFactory)
	{
		ApiFactory = apiFactory;
		Client = new NSwagClient(apiFactory.CreateClient());
	}
	public async Task AuthenticateAsync(string username, string password)
	{
		var response = await Client.LoginAsync(new LoginRequest
		{
			Username = username,
			Password = password
		});
		response.StatusCode.Should().Be(200);
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, response.Result.JwtToken);
	}
}