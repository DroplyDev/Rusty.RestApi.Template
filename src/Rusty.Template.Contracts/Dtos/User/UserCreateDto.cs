using System.Text.RegularExpressions;
using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.User;

/// <summary>
///     The user create dto
/// </summary>
public sealed class UserCreateDto
{
    /// <summary>
    ///     Gets or sets the value of the username
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the value of the password
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the value of the email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the value of the group id
    /// </summary>
    public int? GroupId { get; set; } = null!;
}

/// <summary>
///     The user create dto valdiatior class
/// </summary>
/// <seealso cref="BaseValidator{UserCreateDto}" />
public sealed class UserCreateDtoValdiatior : BaseValidator<UserCreateDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserCreateDtoValdiatior" /> class
    /// </summary>
    public UserCreateDtoValdiatior()
    {
        RuleFor(item => item.UserName)
            .NotNull()
            .MinimumLength(8)
            .MaximumLength(32);
        RuleFor(item => item.Password)
            .NotNull()
            .MinimumLength(8)
            .MaximumLength(32)
            .Must(x =>
            {
                // Use a regular expression to check for at least one uppercase letter, one lowercase letter, and one digit
                var regex = new Regex(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)");
                return regex.IsMatch(x);
            });
        RuleFor(item => item.Email)
            .NotNull()
            .MaximumLength(255)
            .EmailAddress();
        RuleFor(item => item.GroupId)
            .GreaterThan(0)
            .When(item => item.GroupId is not null);
    }
}