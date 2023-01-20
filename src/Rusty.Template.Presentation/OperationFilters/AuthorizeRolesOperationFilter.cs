#region

using Microsoft.OpenApi.Models;
using Rusty.Template.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

public class AuthorizeRolesOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var authAttributes = context.MethodInfo.GetCustomAttributes(true)
			.OfType<AuthorizeRolesAttribute>();
		if (authAttributes.Any())
			operation.TryAddResponse<UnauthorizedAccessException>(context,
				StatusCodes.Status401Unauthorized,
				"You need to authorize with jwt token before accessing secure endpoints"
			);
		// operation.TryAddResponse<AccessViolationException>(context,
		// 	StatusCodes.Status403Forbidden,
		// 	"You do not have access to this endpoint"
		// 	);
	}
}