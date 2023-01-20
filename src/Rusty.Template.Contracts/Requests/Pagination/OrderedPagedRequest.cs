#region

using Rusty.Template.Contracts.SubTypes;

#endregion

// ReSharper disable All

namespace Rusty.Template.Contracts.Requests;

public sealed class OrderedPagedRequest
{
	public PageData? PageData { get; set; }
	public OrderByData OrderByData { get; set; } = null!;
}