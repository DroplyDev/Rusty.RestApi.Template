using Rusty.Template.Contracts.SubTypes;

namespace Rusty.Template.Contracts.Requests;

public sealed record FilteredOrderedPagedRequest
{
    public FilterData? FilterData { get; set; }
    public PageData? PageData { get; set; }
    public OrderByData? OrderByData { get; set; }
}