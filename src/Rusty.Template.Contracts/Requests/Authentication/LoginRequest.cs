#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Requests.Authentication;

[SwaggerSchema("Login request")]
public sealed class LoginRequest
{
	[SwaggerSchema("Username for login")]
	public string Username { get; set; } = null!;

	[SwaggerSchema("Password for login")]
	public string Password { get; set; } = null!;
}