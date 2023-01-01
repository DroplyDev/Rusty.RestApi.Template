using FluentValidation;

namespace Rusty.Template.Contracts.Requests;

/// <summary>
///     The login request
/// </summary>
public sealed record LoginRequest(string Username, string Password);

/// <summary>
///     The login request validator class
/// </summary>
/// <seealso cref="AbstractValidator{LoginRequest}" />
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginRequestValidator" /> class
    /// </summary>
    public LoginRequestValidator()
    {
        RuleFor(item => item.Username).NotEmpty();
        RuleFor(item => item.Password).NotEmpty();
    }
}