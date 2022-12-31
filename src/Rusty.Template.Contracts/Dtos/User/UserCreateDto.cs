using System.Text.RegularExpressions;
using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.User;

public sealed record UserCreateDto(string UserName, string Password, string Email, int? GroupId);

public sealed class UserCreateDtoValdiatior : BaseValidator<UserCreateDto>
{
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