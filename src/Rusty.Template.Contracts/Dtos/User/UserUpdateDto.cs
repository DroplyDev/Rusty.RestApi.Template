using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.User;

/// <summary>
///     The user update dto
/// </summary>
public sealed class UserUpdateDto
{
    /// <summary>
    ///     Gets or sets the value of the email
    /// </summary>
    public string Email { get; set; } = null!;
}

/// <summary>
///     The user update dto validator class
/// </summary>
/// <seealso cref="AbstractValidator{UserUpdateDto}" />
public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserUpdateDtoValidator" /> class
    /// </summary>
    public UserUpdateDtoValidator()
    {
        RuleFor(item => item.Email)
            .NotNull()
            .MaximumLength(255)
            .EmailAddress();
    }
}