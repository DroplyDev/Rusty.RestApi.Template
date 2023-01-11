using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.User;

public sealed class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
	public UserCreateDtoValidator()
	{
		RuleFor(item => item.UserName)
			.MinimumLength(8)
			.MaximumLength(32);
		RuleFor(item => item.Password)
			.MinimumLength(8)
			.Matches("[A-Z]").WithMessage("Password must have at least one upper case letter")
			.Matches("[a-z]").WithMessage("Password must have at least one lower case letter")
			.Matches("[0-9]").WithMessage("Password must have at least one number")
			.Matches("[^a-zA-Z0-9]").WithMessage("Password must have at least one special character")
			.MaximumLength(32);
		RuleFor(item => item.Email)
			.MaximumLength(255)
			.EmailAddress();
		RuleFor(item => item.GroupId)
			.GreaterThan(0)
			.When(item => item.GroupId is not null);
	}
}