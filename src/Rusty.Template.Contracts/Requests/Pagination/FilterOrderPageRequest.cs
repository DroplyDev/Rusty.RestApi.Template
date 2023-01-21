#region

using Rusty.Template.Contracts.SubTypes;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Requests.Pagination;

[SwaggerSchema("Request with filter order by and pagination")]
public sealed class FilterOrderPageRequest
{
	[SwaggerSchema("Filter data class")]
	public FilterData? FilterData { get; set; }

	[SwaggerSchema("Page data class")]
	public PageData? PageData { get; set; }

	[SwaggerSchema("Order by data class")]
	public OrderByData OrderByData { get; set; } = null!;
}