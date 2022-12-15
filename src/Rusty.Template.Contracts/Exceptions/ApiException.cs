namespace Rusty.Template.Contracts.Exceptions;

/// <summary>
///     The api exception class
/// </summary>
/// <seealso cref="Exception" />
public class ApiException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiException" /> class
    /// </summary>
    /// <param name="message">The message</param>
    public ApiException(string message)
    {
        Message = message;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiException" /> class
    /// </summary>
    /// <param name="statusCode">The status code</param>
    protected ApiException(int statusCode)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiException" /> class
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="statusCode">The status code</param>
    public ApiException(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    /// <summary>
    ///     Gets the value of the status code
    /// </summary>
    public int StatusCode { get; } = 500;

    /// <summary>
    ///     Gets the value of the message
    /// </summary>
    public override string Message { get; } = "Unhandled exception occured";
}