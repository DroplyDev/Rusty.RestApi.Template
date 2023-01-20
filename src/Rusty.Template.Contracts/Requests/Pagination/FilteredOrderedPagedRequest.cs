#region

using Rusty.Template.Contracts.SubTypes;

#endregion

namespace Rusty.Template.Contracts.Requests.Pagination;

public sealed class FilteredOrderedPagedRequest
{
	public FilterData? FilterData { get; set; }
	public PageData? PageData { get; set; }
	public OrderByData? OrderByData { get; set; }
}