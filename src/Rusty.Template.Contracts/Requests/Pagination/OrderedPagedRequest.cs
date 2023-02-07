#region

using Rusty.Template.Contracts.SubTypes;
using Swashbuckle.AspNetCore.Annotations;

#endregion

// ReSharper disable All

namespace Rusty.Template.Contracts.Requests;

[SwaggerSchema("Request with order by and pagination")]
public class OrderedPagedRequest
{
	[SwaggerSchema("Page data class")]
	public PageData? PageData { get; init; }

	[SwaggerSchema("Order by data class")]
	public OrderByData OrderByData { get; init; } = null!;
}