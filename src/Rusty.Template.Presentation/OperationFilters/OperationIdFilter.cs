#region

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

/// <summary>
/// Filter that sets method name as operation id
/// </summary>
/// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
public sealed class OperationIdFilter : IOperationFilter
{
	/// <summary>Applies the specified operation.</summary>
	/// <param name="operation">The operation.</param>
	/// <param name="context">The context.</param>
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		operation.OperationId = context.MethodInfo.Name;
	}
}