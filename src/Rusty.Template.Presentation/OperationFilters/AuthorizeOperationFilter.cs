#region

using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

public class AuthorizeOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var attributes = context.ApiDescription.CustomAttributes().Union(context.MethodInfo.GetCustomAttributes(true));
		var authAttributes = attributes.OfType<AuthorizeRolesAttribute>().OfType<AuthorizeAttribute>();
		if (authAttributes.Any())
		{
			operation.TryAddResponse<ApiExceptionResponse>(context,
				StatusCodes.Status401Unauthorized,
				"You need to authorize with jwt token before accessing secure endpoints"
			);
			operation.TryAddResponse<ApiExceptionResponse>(context,
				StatusCodes.Status403Forbidden,
				"You do not have access to this endpoint"
			);
		}
	}
}