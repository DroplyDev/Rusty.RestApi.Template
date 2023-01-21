#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

[SwaggerSchema("Order data subtype")]
public sealed class OrderByData
{
	[SwaggerSchema("Order property name")]
	public string OrderBy { get; set; } = null!;

	[SwaggerSchema("Order direction enum")]
	public OrderDirection OrderDirection { get; set; }
}