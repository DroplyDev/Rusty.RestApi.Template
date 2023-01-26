#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

[SwaggerSchema("Page data subtype")]
public sealed class PageData
{
	[SwaggerSchema("Item offset")]
	public int Offset { get; set; }

	[SwaggerSchema("Item limit")]
	public int Limit { get; set; }
}