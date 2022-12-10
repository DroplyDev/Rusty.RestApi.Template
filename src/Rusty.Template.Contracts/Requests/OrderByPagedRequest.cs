using Rusty.Template.Contracts.SubTypes;

// ReSharper disable All

namespace Rusty.Template.Contracts.Requests;

public sealed record OrderByPagedRequest
{
    public PageData? PageData { get; set; }
    public OrderByData? OrderByData { get; set; }
}