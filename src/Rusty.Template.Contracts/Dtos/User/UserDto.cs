using Rusty.Template.Contracts.Dtos.Group;

namespace Rusty.Template.Contracts.Dtos.User;

/// <summary>
///     The user dto
/// </summary>
public sealed record UserDto
{
    /// <summary>
    ///     Gets or sets the value of the id
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    ///     Gets or sets the value of the name
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    ///     Gets or sets the value of the email
    /// </summary>
    public string Email { get; init; } = null!;

    /// <summary>
    ///     Gets or sets the value of the group dto
    /// </summary>
    public GroupDto? GroupDto { get; init; }
}