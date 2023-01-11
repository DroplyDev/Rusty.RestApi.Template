using Swashbuckle.AspNetCore.Annotations;

namespace Rusty.Template.Contracts.Dtos.Group;

[SwaggerSchema("The dto for user retrieval ")]

public sealed record GroupDto
{
    [SwaggerSchema("The group id")]
    public int Id { get; init; }

    [SwaggerSchema("The group name")]

    public string Name { get; init; } = null!;
}