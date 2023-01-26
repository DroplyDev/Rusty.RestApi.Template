#region

using Rusty.Template.Contracts.Dtos.Group;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.User;

[SwaggerSchema("The dto for user retrieval ")]
public sealed record UserDto([SwaggerSchema("The user id")] int Id,
							 [SwaggerSchema("The user name")] string Username,
							 [SwaggerSchema("The user email")] string Email,
							 [SwaggerSchema("The user group")] GroupDto? GroupDto);