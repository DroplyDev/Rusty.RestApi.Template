#region

using Rusty.Template.Contracts.SubTypes;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Requests.Pagination;

[SwaggerSchema("Request with filter order by and pagination")]
public sealed class FilterOrderPageRequest : OrderedPagedRequest
{
	[SwaggerSchema("Filter data class")]
	public FilterData? FilterData { get; init; }
}