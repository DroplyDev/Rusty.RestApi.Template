using Serilog.Events;

namespace Rusty.Template.Contracts.Exceptions;

/// <summary>
///     The api exception class
/// </summary>
/// <seealso cref="Exception" />
public class ApiException : Exception
{
    /// <summary>
    ///     Gets or sets the value of the log level
    /// </summary>
    private readonly LogEventLevel _logLevel = LogEventLevel.Fatal;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiException" /> class
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="statusCode">The status code</param>
    /// <param name="logLevel"></param>
    public ApiException(string message, int statusCode, LogEventLevel logLevel) : base(message)
    {
        StatusCode = statusCode;
        _logLevel = logLevel;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiException" /> class
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="statusCode">The status code</param>
    /// <param name="logLevel">The log level</param>
    /// <param name="innerException">The inner exception</param>
    public ApiException(string message, int statusCode, LogEventLevel logLevel, Exception innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        _logLevel = logLevel;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiException" /> class
    /// </summary>
    public ApiException()
    {
    }

    /// <summary>
    ///     Gets the value of the status code
    /// </summary>
    public int StatusCode { get; } = 500;

    /// <summary>
    ///     Gets the value of the message
    /// </summary>
    public override string Message { get; } = "Unhandled exception occured";

    /// <summary>
    ///     Gets the level
    /// </summary>
    /// <returns>The log level</returns>
    public LogEventLevel GetLevel()
    {
        return _logLevel;
    }
}