namespace Rusty.Template.Contracts.Exceptions;

public class ApiException : Exception
{
    public ApiException(string message)
    {
        Message = message;
    }

    protected ApiException(int statusCode)
    {
        StatusCode = statusCode;
    }

    public ApiException(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    public int StatusCode { get; } = 500;
    public override string Message { get; } = "Unhandled exception occured";
}