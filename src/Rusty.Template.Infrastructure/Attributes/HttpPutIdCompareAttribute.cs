#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace Rusty.Template.Infrastructure.Attributes;

public sealed class HttpPutIdCompareAttribute : ActionFilterAttribute
{
	private readonly string _dtoPropertyName;
	private readonly string _queryPropertyName;

	public HttpPutIdCompareAttribute(string dtoPropertyName, string queryPropertyName)
	{
		_dtoPropertyName = dtoPropertyName;
		_queryPropertyName = queryPropertyName;
	}

	public HttpPutIdCompareAttribute(string dtoPropertyName)
	{
		_dtoPropertyName = dtoPropertyName;
		_queryPropertyName = _dtoPropertyName.ToLower();
	}

	public HttpPutIdCompareAttribute()
	{
		_dtoPropertyName = "Id";
		_queryPropertyName = "id";
	}

	public override void OnActionExecuting(ActionExecutingContext context)
	{
		var model = context.ActionArguments.Values.First(item => item!.GetType().IsClass);
		// Use reflection to get the value of the specified property
		var propertyInfo = model!.GetType().GetProperty(_dtoPropertyName);
		var propertyValue = (int)propertyInfo!.GetValue(model)!;
		// Get the route id and model id from the action arguments
		var routeId = (int)context.ActionArguments[_queryPropertyName]!;
		// Ensure that the route id matches the model id
		if (routeId != propertyValue) context.Result = new BadRequestObjectResult("Route id does not match model id");
	}
}