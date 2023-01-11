using Swashbuckle.AspNetCore.Annotations;

namespace Rusty.Template.Contracts.Dtos.Group;

[SwaggerSchema("The dto for group update")]
public sealed class GroupUpdateDto
{
    [SwaggerSchema("The group name")]

    public string Name { get; set; } = null!;
}

