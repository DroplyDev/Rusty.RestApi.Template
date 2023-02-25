#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.Group;

/// <summary>
/// The dto for group update.
/// </summary>
public sealed class GroupUpdateDto
{
	/// <summary>The group name.</summary>
	/// <example>TestGroupName</example>
	public string Name { get; set; } = null!;
}