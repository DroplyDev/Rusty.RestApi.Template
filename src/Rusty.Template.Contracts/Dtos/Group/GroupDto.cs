#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Dtos.Group;

[SwaggerSchema("The dto for user retrieval ")]
public sealed record GroupDto([SwaggerSchema("The group id")] int Id,
							  [SwaggerSchema("The group name")] string Name);