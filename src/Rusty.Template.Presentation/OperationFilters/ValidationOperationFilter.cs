#region

using Microsoft.OpenApi.Models;
using Rusty.Template.Contracts.Responses;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

/// <summary>
/// Filter that generates responses for validation
/// </summary>
/// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
public sealed class ValidationOperationFilter : IOperationFilter
{
	/// <summary>Applies the specified operation.</summary>
	/// <param name="operation">The operation.</param>
	/// <param name="context">The context.</param>
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		if (context.ApiDescription.HttpMethod == "POST" || context.ApiDescription.HttpMethod == "PUT")
			operation.TryAddResponse<ApiValidationResult>(context,
				StatusCodes.Status400BadRequest,
				"Model validation exception"
			);
	}
}