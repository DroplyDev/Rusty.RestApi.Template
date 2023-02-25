#region

using Rusty.Template.Contracts.SubTypes;
using Swashbuckle.AspNetCore.Annotations;

#endregion

// ReSharper disable All

namespace Rusty.Template.Contracts.Requests;

/// <summary>
/// Request with order by and pagination
/// </summary>
public class OrderedPagedRequest
{
	/// <summary>Page data class.</summary>
	public PageData? PageData { get; init; }

	/// <summary>Order by data class.</summary>
	public OrderByData OrderByData { get; init; } = null!;
}