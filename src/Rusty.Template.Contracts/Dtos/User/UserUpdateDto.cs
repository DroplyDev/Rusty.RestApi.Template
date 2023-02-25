#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.User;

/// <summary>
/// The dto for user edit
/// </summary>
public sealed class UserUpdateDto
{
	/// <summary>The user id. Must be the same as in request.</summary>
	/// <example>1</example>
	public int Id { get; set; }
	/// <summary>The user email.</summary>
	/// <example>Test@example.com</example>
	public string Email { get; set; } = null!;
	/// <summary>The user group id.</summary>
	/// <example>1</example>
	public string? GroupId { get; set; }
}