#region

using Rusty.Template.Contracts.Dtos.Group;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.User;

/// <summary>
/// The dto for user retrieval.
/// </summary>
public sealed record UserDto
{
	/// <summary>The user id.</summary>
	/// <example>1</example>
	public int Id { get; init; }
	/// <summary>The user name.</summary>
	/// <example>ExampleUser</example>
	public string UserName { get; init; } = null!;
	/// <summary>The user first name.</summary>
	/// <example>John</example>
	public string? FirstName { get; init; }
	/// <summary>The user last name.</summary>
	/// <example>Doe</example>
	public string? LastName { get; init; }
	/// <summary>The user email.</summary>
	/// <example>Test@example.com</example>
	public string Email { get; init; } = null!;
	/// <summary>Gets the group dto.</summary>
	public GroupDto? GroupDto { get; init; }
}