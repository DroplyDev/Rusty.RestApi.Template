#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

public class ValidationOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var authAttributes = context.MethodInfo.DeclaringType!.GetCustomAttributes(true)
									.Union(context.MethodInfo.GetCustomAttributes(true))
									.OfType<HttpPostAttribute>().Union(context.MethodInfo.GetCustomAttributes(true))
									.OfType<HttpPutAttribute>();
		if (authAttributes.Any())
			operation.Responses.TryAdd(StatusCodes.Status400BadRequest.ToString(),
				new OpenApiResponse
					{ Description = "You need to authorize with jwt token before accessing secure endpoints" });
	}
}