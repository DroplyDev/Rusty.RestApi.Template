#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.Requests;

public sealed record LoginRequest(string Username, string Password);

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
	public LoginRequestValidator()
	{
		RuleFor(item => item.Username).NotEmpty();
		RuleFor(item => item.Password).NotEmpty();
	}
}