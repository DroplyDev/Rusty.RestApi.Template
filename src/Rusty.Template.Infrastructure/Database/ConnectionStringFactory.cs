using Rusty.Template.Domain.Exceptions;

namespace Rusty.Template.Infrastructure.Database;

/// <summary>
///     The connection string factory class
/// </summary>
public class ConnectionStringFactory
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ConnectionStringFactory" /> class
    /// </summary>
    /// <param name="connectionString">The connection string</param>
    public ConnectionStringFactory(string? connectionString)
    {
        ConnectionString = connectionString ?? throw new ConnectionStringIsNullException();
    }

    /// <summary>
    ///     Gets the value of the connection string
    /// </summary>
    public string ConnectionString { get; }
}