using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rusty.Template.Infrastructure.Attributes;

/// <summary>
///     The http put id compare attribute class
/// </summary>
/// <seealso cref="ActionFilterAttribute" />
public class HttpPutIdCompareAttribute : ActionFilterAttribute
{
    /// <summary>
    ///     The property name
    /// </summary>
    private readonly string _propertyName;

    /// <summary>
    ///     Initializes a new instance of the <see cref="HttpPutIdCompareAttribute" /> class
    /// </summary>
    /// <param name="propertyName">The property name</param>
    public HttpPutIdCompareAttribute(string propertyName)
    {
        _propertyName = propertyName;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="HttpPutIdCompareAttribute" /> class
    /// </summary>
    public HttpPutIdCompareAttribute()
    {
        _propertyName = "Id";
    }

    /// <summary>
    ///     Ons the action executing using the specified context
    /// </summary>
    /// <param name="context">The context</param>
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