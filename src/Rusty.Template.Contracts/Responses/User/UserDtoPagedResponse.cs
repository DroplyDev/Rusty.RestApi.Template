#region

using Rusty.Template.Contracts.Dtos.User;

#endregion

namespace Rusty.Template.Contracts.Responses.User;

public sealed record UserDtoPagedResponse(List<UserDto> Data, int TotalCount)
	: PagedResponse<UserDto>(Data, TotalCount);