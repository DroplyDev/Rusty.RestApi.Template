using Serilog.Events;

namespace Rusty.Template.Contracts.Exceptions.Entity;

/// <summary>
///     The order param name not valid exception class
/// </summary>
/// <seealso cref="ApiException" />
public class EntityOrderParamNameNotValidException<TEntity> : BaseEntityException<TEntity> where TEntity : class
{
    /// <summary>
    ///     Initializes a new instance of the <see /> class
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="field">The field</param>
    public EntityOrderParamNameNotValidException(string message, string field) : base(message, 400,
        LogEventLevel.Warning)
    {
        Field = field;
    }

    /// <summary>
    ///     Initializes a new instance of the <see /> class
    /// </summary>
    /// <param name="field">The field</param>
    public EntityOrderParamNameNotValidException(string field) : base(
        $@"Order by procedure failed. Field: {field} was not found in in response dto", 400, LogEventLevel.Warning)
    {
        Field = field;
    }

    /// <summary>
    ///     Gets the value of the field
    /// </summary>
    public string Field { get; }
}