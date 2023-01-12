#region

using Rusty.Template.Contracts.Dtos.User;

#endregion

namespace Rusty.Template.Contracts.Responses.User;

public sealed record UserPagedResponse(List<UserDto> Data, int TotalCount)
	: PagedResponse<UserDto>(Data, TotalCount);