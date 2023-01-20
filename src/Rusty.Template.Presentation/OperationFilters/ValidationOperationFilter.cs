#region

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

public class ValidationOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		operation.Responses ??= new OpenApiResponses();
		if (context.ApiDescription.HttpMethod == "POST" || context.ApiDescription.HttpMethod == "PUT")
			operation.TryAddResponse<AccessViolationException>(context,
				StatusCodes.Status400BadRequest,
				"Model validation exception"
			);
	}
}