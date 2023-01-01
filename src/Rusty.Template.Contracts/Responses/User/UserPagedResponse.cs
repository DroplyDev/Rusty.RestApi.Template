using Rusty.Template.Contracts.Dtos.User;

namespace Rusty.Template.Contracts.Responses.User;

/// <summary>
///     The user paged response
/// </summary>
public sealed record UserPagedResponse(List<UserDto> Data, int TotalCount)
    : PagedResponse<UserDto>(Data, TotalCount);