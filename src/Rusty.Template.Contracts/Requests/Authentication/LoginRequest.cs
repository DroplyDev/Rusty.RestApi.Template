#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Requests.Authentication;

/// <summary>
/// Login request
/// </summary>
public sealed class LoginRequest
{
	/// <summary>The user name</summary>
	/// <example>John</example>
	[SwaggerSchema("Username for login")]
	public string Username { get; set; } = null!;

	/// <summary>The user password. Has validation.</summary>
	/// <example>Qwerty123$</example>
	[SwaggerSchema("Password for login")]
	public string Password { get; set; } = null!;
}