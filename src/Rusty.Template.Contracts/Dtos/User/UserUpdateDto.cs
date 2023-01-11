using Swashbuckle.AspNetCore.Annotations;

namespace Rusty.Template.Contracts.Dtos.User;

[SwaggerSchema("The dto for user edit")]
public sealed class UserUpdateDto
{
    [SwaggerSchema("The user email")]
    public string Email { get; set; } = null!;
}
