#region

using System.Net;
using Microsoft.AspNetCore.Http;
using Rusty.Template.Contracts.Exceptions;
using Serilog;
using Serilog.Events;

#endregion

namespace Rusty.Template.Infrastructure.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
	private readonly ILogger _logger;
	private readonly RequestDelegate _next;

	public ExceptionHandlingMiddleware(ILogger logger, RequestDelegate next)
	{
		_logger = logger;
		_next = next;
	}


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


	private static Task HandleExceptionAsync(HttpContext context, ApiException exception)
	{
		context.Response.StatusCode = exception.StatusCode;
		context.Response.ContentType = "application/json";
		return context.Response.WriteAsJsonAsync(exception.ToString());
	}


	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		context.Response.ContentType = "application/json";
		return context.Response.WriteAsJsonAsync(
			new ApiException(exception.Message, context.Response.StatusCode, LogEventLevel.Fatal).ToString());
	}
}