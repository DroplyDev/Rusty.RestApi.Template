using Serilog.Events;

namespace Rusty.Template.Contracts.Exceptions;

/// <summary>
///     The insufficient privilege exception class
/// </summary>
/// <seealso cref="ApiException" />
public class InsufficientPrivilegeException : ApiException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InsufficientPrivilegeException" /> class
    /// </summary>
    /// <param name="message">The message</param>
    protected InsufficientPrivilegeException(string message) : base(message, 403, LogEventLevel.Warning)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="InsufficientPrivilegeException" /> class
    /// </summary>
    public InsufficientPrivilegeException() : base("Permission denied", 403, LogEventLevel.Warning)
    {
    }
}