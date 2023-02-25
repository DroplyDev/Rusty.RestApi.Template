#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.Dtos.Group;

/// <summary>
/// GroupCreateDtoValidator
/// </summary>
public sealed class GroupCreateDtoValidator : AbstractValidator<GroupCreateDto>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="GroupCreateDtoValidator" /> class.
	/// </summary>
	public GroupCreateDtoValidator()
	{
		RuleFor(w => w.Name)
			.MaximumLength(32);
	}
}