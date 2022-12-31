namespace Rusty.Template.Contracts.Dtos.User;

public sealed record UserUpdateDto(string data);

public class UserUpdateDtoValidator : BaseValidator<UserUpdateDto>
{
    

}
