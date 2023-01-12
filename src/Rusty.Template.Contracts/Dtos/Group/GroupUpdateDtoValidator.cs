#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.Dtos.Group;

public sealed class GroupUpdateDtoValidator : AbstractValidator<GroupUpdateDto>
{
	public GroupUpdateDtoValidator()
	{
		RuleFor(item => item.Name)
			.MaximumLength(32);
	}
}