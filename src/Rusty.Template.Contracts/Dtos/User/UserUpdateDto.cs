#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.User;

[SwaggerSchema("The dto for user edit")]
public sealed class UserUpdateDto
{
	[SwaggerSchema("The user id. Must be the same as in request")]
	public int Id { get; set; }

	[SwaggerSchema("The user email")]
	public string Email { get; set; } = null!;
}