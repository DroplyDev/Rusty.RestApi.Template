using Swashbuckle.AspNetCore.Annotations;

namespace Rusty.Template.Contracts.Dtos.Group;

[SwaggerSchema("The dto for group create")]
public sealed class GroupCreateDto
{
    [SwaggerSchema("The group name")]
    public string Name { get; set; } = null!;
}
