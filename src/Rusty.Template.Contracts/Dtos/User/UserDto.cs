#region

using Rusty.Template.Contracts.Dtos.Group;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.User;

[SwaggerSchema("The dto for user retrieval ")]
public sealed record UserDto
{
	[SwaggerSchema("The user id")] public int Id { get; init; }

	[SwaggerSchema("The user name")] public string UserName { get; init; } = null!;

	[SwaggerSchema("The user email")] public string Email { get; init; } = null!;

	[SwaggerSchema("The user group")] public GroupDto? GroupDto { get; init; }
}