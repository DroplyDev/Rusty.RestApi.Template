using Serilog.Events;

namespace Rusty.Template.Contracts.Exceptions.Entity;

/// <summary>
///     The base entity exception class
/// </summary>
/// <seealso cref="ApiException" />
public abstract class BaseEntityException<TEntity> : ApiException where TEntity : class
{
    /// <summary>
    ///     Initializes a new instance of the <see /> class
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="statusCode">The status code</param>
    /// <param name="logEventLevel">The log event level</param>
    protected BaseEntityException(string message, int statusCode, LogEventLevel logEventLevel) : base(message,
        statusCode, logEventLevel)
    {
    }
}