using Microsoft.OpenApi.Models;
using Rusty.Template.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rusty.Template.Presentation.OperationFilters;

public class AuthorizeRolesOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var authAttributes = context.MethodInfo.DeclaringType!.GetCustomAttributes(true)
		                            .Union(context.MethodInfo.GetCustomAttributes(true))
		                            .OfType<AuthorizeRolesAttribute>();

		if (authAttributes.Any())
		{
			operation.Responses.TryAdd(StatusCodes.Status401Unauthorized.ToString(),
				new OpenApiResponse
					{ Description = "You need to authorize with jwt token before accessing secure endpoints" });
			operation.Responses.TryAdd(StatusCodes.Status403Forbidden.ToString(),
				new OpenApiResponse { Description = "You do not have access to this endpoint" });
		}
	}
}