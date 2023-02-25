#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.Group;

/// <summary>The dto for group create.</summary>
public sealed class GroupCreateDto
{
	/// <summary>The group name.</summary>
	/// <example>TestGroupName</example>
	public string Name { get; set; } = null!;
}