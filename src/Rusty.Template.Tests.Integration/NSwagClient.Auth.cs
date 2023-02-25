using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rusty.Template.Tests.Integration;
public partial class NSwagClient
{
	public async Task AuthenticateAsync(string username, string password)
	{
		var response = await LoginAsync(new LoginRequest
		{
			Username = username,
			Password = password
		});
		response.StatusCode.Should().Be(200);
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, response.Result.JwtToken);
	}
}
