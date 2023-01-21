#region

using Microsoft.OpenApi.Models;
using Rusty.Template.Contracts.Responses;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

public class InternalServerErrorResponseFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		operation.TryAddResponse<ApiExceptionResponse>(context,
			StatusCodes.Status500InternalServerError,
			"Internal server error"
		);
	}
}