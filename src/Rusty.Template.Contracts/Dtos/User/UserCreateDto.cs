#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.User;

/// <summary>
/// The dto for user creation.
/// </summary>
public sealed class UserCreateDto
{
	/// <summary>The user name</summary>
	/// <example>ExampleUser</example>
	public string UserName { get; set; } = null!;

	/// <summary>The user password. Has validation.</summary>
	/// <example>Qwerty123$</example>
	public string Password { get; set; } = null!;

	/// <summary>The user email.</summary>
	/// <example>Test@example.com</example>
	public string Email { get; set; } = null!;

	/// <summary>The user group id.</summary>
	/// <example>1</example>
	public int? GroupId { get; set; } = null!;
}