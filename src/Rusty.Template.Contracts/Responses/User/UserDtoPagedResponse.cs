#region

using Rusty.Template.Contracts.Dtos.User;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Responses.User;

[SwaggerSchema("User paged payload response")]
public sealed record UserDtoPagedResponse(List<UserDto> Data, int TotalCount)
	: PagedResponse<UserDto>(Data, TotalCount);