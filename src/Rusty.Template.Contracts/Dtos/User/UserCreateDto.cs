using System.Text.RegularExpressions;
using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.User;

/// <summary>
///     The user create dto
/// </summary>
public sealed record UserCreateDto(string UserName, string Password, string Email, int? GroupId);

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
            .MinimumLength(8)
            .MaximumLength(32);
        RuleFor(item => item.Password)
            .MinimumLength(8)
            .MaximumLength(32)
            .Must(x =>
            {
                // Use a regular expression to check for at least one uppercase letter, one lowercase letter, and one digit
                var regex = new Regex(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)");
                return regex.IsMatch(x);
            });
        RuleFor(item => item.Email)
            .EmailAddress();
        RuleFor(item => item.GroupId)
            .GreaterThan(0)
            .When(item => item.GroupId is not null);
    }
}