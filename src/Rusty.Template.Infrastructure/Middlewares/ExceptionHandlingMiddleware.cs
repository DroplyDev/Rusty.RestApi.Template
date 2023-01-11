using System.Net;
using Microsoft.AspNetCore.Http;
using Rusty.Template.Contracts.Exceptions;
using Serilog;
using Serilog.Events;

namespace Rusty.Template.Infrastructure.Middlewares;

/// <summary>
///     The exception handling middleware class
/// </summary>
public sealed class ExceptionHandlingMiddleware
{
    /// <summary>
    ///     The logger
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    ///     The next
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExceptionHandlingMiddleware" /> class
    /// </summary>
    /// <param name="logger">The logger</param>
    /// <param name="next">The next</param>
    public ExceptionHandlingMiddleware(ILogger logger, RequestDelegate next)
    {
	    _logger = logger;
	    _next = next;
    }

    /// <summary>
    ///     Invokes the context
    /// </summary>
    /// <param name="context">The context</param>
    public async Task InvokeAsync(HttpContext context)
    {
	    try
	    {
		    await _next(context);
	    }
	    catch (ApiException ex)
	    {
		    _logger.Write(ex.GetLevel(), ex, ex.Description);
		    await HandleExceptionAsync(context, ex);
	    }
	    catch (Exception ex)
	    {
		    _logger.Fatal(ex, ex.Message);
		    await HandleExceptionAsync(context, ex);
	    }
    }

    /// <summary>
    ///     Handles the exception using the specified context
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="exception">The exception</param>
    private static Task HandleExceptionAsync(HttpContext context, ApiException exception)
    {
	    context.Response.StatusCode = exception.StatusCode;
	    context.Response.ContentType = "application/json";
	    return context.Response.WriteAsJsonAsync(exception.ToString());
    }

    /// <summary>
    ///     Handles the exception using the specified context
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="exception">The exception</param>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
	    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
	    context.Response.ContentType = "application/json";
	    return context.Response.WriteAsJsonAsync(
		    new ApiException(exception.Message, context.Response.StatusCode, LogEventLevel.Fatal).ToString());
    }
}