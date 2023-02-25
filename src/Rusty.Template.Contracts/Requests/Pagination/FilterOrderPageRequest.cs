#region

using Rusty.Template.Contracts.SubTypes;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Requests.Pagination;

/// <summary>
/// Request with filter order by and pagination
/// </summary>
public sealed class FilterOrderPageRequest : OrderedPagedRequest
{
	/// <summary>Filter data class.</summary>
	public FilterData? FilterData { get; init; }
}