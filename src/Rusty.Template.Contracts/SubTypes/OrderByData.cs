#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>
/// Order data subtype
/// </summary>
public sealed class OrderByData
{
	/// <summary>Order property name.</summary>
	/// <example>FieldName</example>

	public string OrderBy { get; set; } = null!;

	/// <summary>Order direction enum.</summary>
	public OrderDirection OrderDirection { get; set; }
}