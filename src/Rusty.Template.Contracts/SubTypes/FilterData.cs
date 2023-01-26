#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

[SwaggerSchema("Filter data subtype")]
public sealed class FilterData
{
	[SwaggerSchema("Start date filter")]
	public DateTime DateFrom { get; set; }

	[SwaggerSchema("End date filter")]
	public DateTime DateTo { get; set; }
}