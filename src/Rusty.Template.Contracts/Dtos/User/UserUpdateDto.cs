namespace Rusty.Template.Contracts.Dtos.User;

/// <summary>
///     The user update dto
/// </summary>
public sealed record UserUpdateDto(string data);

/// <summary>
///     The user update dto validator class
/// </summary>
/// <seealso cref="BaseValidator{UserUpdateDto}" />
public class UserUpdateDtoValidator : BaseValidator<UserUpdateDto>
{
}