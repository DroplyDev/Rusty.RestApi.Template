using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rusty.Template.Contracts.Exceptions;
using Rusty.Template.Contracts.Exceptions.Entity;
using Rusty.Template.Domain;
using Serilog;

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


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await _next(context);
        }
        catch (EntityWithIdAlreadyExistsException<BaseEntity> ex)
        {
            _logger.Information(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
        catch (EntityNotFoundByIdException<BaseEntity> ex)
        {
            _logger.Information(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
        catch (EntityValidationException<BaseEntity> ex)
        {
            _logger.Information(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
        catch (InsufficientPrivilegeException ex)
        {
            _logger.Information(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
        catch (NegativeValueException ex)
        {
            _logger.Information(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
        catch (OrderParamNameNotValidException ex)
        {
            _logger.Information(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.Information(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
        catch (ApiException ex)
        {
            _logger.Error(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.Fatal(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, ApiException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(exception.ToString());
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsJsonAsync(new ApiException(exception.Message, context.Response.StatusCode)
            .ToString());
    }
}