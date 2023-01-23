#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.Requests.Authentication;

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
	public LoginRequestValidator()
	{
		RuleFor(item => item.Username)
			.NotEmpty();
		RuleFor(item => item.Password)
			.NotEmpty();
	}
}