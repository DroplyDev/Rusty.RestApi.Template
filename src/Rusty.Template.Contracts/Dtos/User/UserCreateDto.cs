#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.User;

[SwaggerSchema("The dto for user creation")]
public sealed class UserCreateDto
{
	[SwaggerSchema("The user name. Has validation")]
	public string UserName { get; set; } = null!;

	[SwaggerSchema("The user password. Has validation")]

	public string Password { get; set; } = null!;

	[SwaggerSchema("The user email")]
	public string Email { get; set; } = null!;

	[SwaggerSchema("The user group id")]
	public int? GroupId { get; set; } = null!;
}