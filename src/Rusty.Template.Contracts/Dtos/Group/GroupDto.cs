#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.Group;

/// <summary>
///   The dto for user retrieval
/// </summary>
public sealed record GroupDto
{
	/// <summary>
	/// Group's unique id.
	/// </summary>
	/// <example>1</example>
	public int Id { get; init; }
	/// <summary>
	/// Group's name.
	/// </summary>
	/// <example>TestGroupName</example>
	public string Name { get; init; } = null!;
}