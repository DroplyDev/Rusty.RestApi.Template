#region

using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

public sealed class ValidationOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		if (context.ApiDescription.HttpMethod == "POST" || context.ApiDescription.HttpMethod == "PUT")
			operation.TryAddResponse<ValidationResult>(context,
				StatusCodes.Status400BadRequest,
				"Model validation exception"
			);
	}
}