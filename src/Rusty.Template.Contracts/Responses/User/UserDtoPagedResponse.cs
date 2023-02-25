#region

using Rusty.Template.Contracts.Dtos.User;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Responses.User;

/// <summary>
/// User paged payload response
/// </summary>
public sealed record UserDtoPagedResponse(IEnumerable<UserDto> Data, int TotalCount)
	: PagedResponse<UserDto>(Data, TotalCount);