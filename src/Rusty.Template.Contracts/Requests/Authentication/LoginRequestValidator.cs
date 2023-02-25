#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.Requests.Authentication;

/// <summary>
/// LoginRequestValidator
/// </summary>
public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
	/// <summary>Initializes a new instance of the <see cref="LoginRequestValidator"/> class.</summary>
	public LoginRequestValidator()
	{
		RuleFor(item => item.Username)
			.NotEmpty();
		RuleFor(item => item.Password)
			.NotEmpty();
	}
}