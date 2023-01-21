#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Responses;

[SwaggerSchema("Paged payload response")]
public record PagedResponse<TEntity>([SwaggerSchema("Paged data")] List<TEntity> Data,
									 [SwaggerSchema("Total record count without pagination")]
									 int TotalCount);