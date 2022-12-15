namespace Rusty.Template.Contracts.Exceptions;

/// <summary>
///     The order param name not valid exception class
/// </summary>
/// <seealso cref="ApiException" />
public class OrderParamNameNotValidException : ApiException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OrderParamNameNotValidException" /> class
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="field">The field</param>
    public OrderParamNameNotValidException(string message, string field) : base(message)
    {
        Field = field;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OrderParamNameNotValidException" /> class
    /// </summary>
    /// <param name="field">The field</param>
    public OrderParamNameNotValidException(string field) : base($"field: {field} was not found in database", 400)
    {
        Field = field;
    }

    /// <summary>
    ///     Gets the value of the field
    /// </summary>
    public string Field { get; }
}