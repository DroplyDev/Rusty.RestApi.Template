namespace Rusty.Template.Contracts.Dtos.Group;

/// <summary>
///     The group dto
/// </summary>
public sealed record GroupDto
{
    /// <summary>
    ///     Gets or sets the value of the id
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    ///     Gets or sets the value of the group name
    /// </summary>
    public string Name { get; init; } = null!;
}