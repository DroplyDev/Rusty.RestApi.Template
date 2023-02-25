#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.Dtos.User;

/// <summary>
/// UserCreateDtoValidator
/// </summary>
public sealed class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
	/// <summary>Initializes a new instance of the <see cref="UserUpdateDtoValidator"/> class.</summary>
	public UserUpdateDtoValidator()
	{
		RuleFor(item => item.Id)
			.GreaterThanOrEqualTo(0);
		RuleFor(item => item.Email)
			.MaximumLength(255)
			.EmailAddress();
	}
}