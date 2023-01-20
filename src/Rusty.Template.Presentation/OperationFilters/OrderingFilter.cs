#region

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

public class OrderingFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
		var actionName = controllerActionDescriptor!.ActionName;
		var controllerName = controllerActionDescriptor.ControllerName;
		operation.OperationId = $"{controllerName}_{actionName}";
	}
}