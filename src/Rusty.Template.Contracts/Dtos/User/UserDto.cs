using Rusty.Template.Contracts.Dtos.Group;

namespace Rusty.Template.Contracts.Dtos.User;

/// <summary>
///     The user dto
/// </summary>
public sealed record UserDto(int Id, string Username, string Email, GroupDto GroupDto);