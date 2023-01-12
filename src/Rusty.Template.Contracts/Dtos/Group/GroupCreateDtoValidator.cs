#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.Dtos.Group;

public sealed class GroupCreateDtoValidator : AbstractValidator<GroupCreateDto>
{
	public GroupCreateDtoValidator()
	{
		RuleFor(w => w.Name)
			.MaximumLength(32);
	}
}