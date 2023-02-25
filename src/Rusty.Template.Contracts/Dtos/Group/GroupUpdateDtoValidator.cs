#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.Dtos.Group;

/// <summary>
/// GroupUpdateDtoValidator
/// </summary>
public sealed class GroupUpdateDtoValidator : AbstractValidator<GroupUpdateDto>
{
	/// <summary>Initializes a new instance of the <see cref="GroupUpdateDtoValidator"/> class.</summary>
	public GroupUpdateDtoValidator()
	{
		RuleFor(item => item.Name)
			.MaximumLength(32);
	}
}