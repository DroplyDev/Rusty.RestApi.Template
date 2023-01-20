#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

[SwaggerSchema("Order Direction")]
public enum OrderDirection
{
	Asc = 1,
	Desc = 2
}