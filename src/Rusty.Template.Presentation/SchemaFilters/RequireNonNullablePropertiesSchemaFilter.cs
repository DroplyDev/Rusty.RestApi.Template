#region

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.SchemaFilters;

/// <summary>Schema filter for non nullable properties</summary>
/// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter" />
public sealed class RequireNonNullablePropertiesSchemaFilter : ISchemaFilter
{
	/// <summary>Applies the specified schema.</summary>
	/// <param name="schema">The schema.</param>
	/// <param name="context">The context.</param>
	public void Apply(OpenApiSchema schema, SchemaFilterContext context)
	{
		var additionalRequiredProps = schema.Properties
			.Where(x => !x.Value.Nullable && !schema.Required.Contains(x.Key))
			.Select(x => x.Key);
		foreach (var propKey in additionalRequiredProps)
			schema.Required.Add(propKey);
	}
}