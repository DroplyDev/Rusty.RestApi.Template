using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rusty.Template.Presentation.SchemaFilters;

/// <summary>
///     The enum schema filter class
/// </summary>
/// <seealso cref="ISchemaFilter" />
public class EnumSchemaFilter : ISchemaFilter
{
    /// <summary>
    ///     Applies the model
    /// </summary>
    /// <param name="model">The model</param>
    /// <param name="context">The context</param>
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) return;

        model.Type = "string";
        model.Enum.Clear();

        Enum.GetNames(context.Type)
            .ToList()
            .ForEach(name => model.Enum.Add(new OpenApiString(name)));
    }
}