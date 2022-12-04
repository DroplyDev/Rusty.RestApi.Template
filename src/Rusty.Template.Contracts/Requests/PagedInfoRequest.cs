using Rusty.Template.Contracts.SubTypes;

namespace Rusty.Template.Contracts.Requests;

public record PagedInfoRequest
{
    public PageData? PageData { get; set; }
    public OrderByData? OrderByData { get; set; }
}