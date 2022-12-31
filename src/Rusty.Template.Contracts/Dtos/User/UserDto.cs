using Rusty.Template.Contracts.Dtos.Group;

namespace Rusty.Template.Contracts.Dtos.User;

public sealed record UserDto(int Id, string Username, string Email, GroupDto GroupDto);