﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rusty.Template.Infrastructure.Attributes;

public class HttpPutIdCompareAttribute : ActionFilterAttribute
{
    private readonly string _propertyName;

    public HttpPutIdCompareAttribute(string propertyName)
    {
        _propertyName = propertyName;
    }

    public HttpPutIdCompareAttribute()
    {
        _propertyName = "Id";
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var model = context.ActionArguments.Values.First(item => item!.GetType().IsClass);
        // Use reflection to get the value of the specified property
        var propertyInfo = model!.GetType().GetProperty(_propertyName);
        var propertyValue = (int)propertyInfo!.GetValue(model)!;
        // Get the route id and model id from the action arguments
        var routeId = (int)context.ActionArguments["id"]!;
        // Ensure that the route id matches the model id
        if (routeId != propertyValue) context.Result = new BadRequestObjectResult("Route id does not match model id");
    }
}