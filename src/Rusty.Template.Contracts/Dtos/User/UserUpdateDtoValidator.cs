#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.Dtos.User;

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
	public UserUpdateDtoValidator()
	{
		RuleFor(item => item.Email)
			.MaximumLength(255)
			.EmailAddress();
	}
}