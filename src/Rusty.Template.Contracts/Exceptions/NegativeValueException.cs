namespace Rusty.Template.Contracts.Exceptions;

/// <summary>
///     The negative value exception class
/// </summary>
/// <seealso cref="ApiException" />
public class NegativeValueException : ApiException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="NegativeValueException" /> class
    /// </summary>
    /// <param name="message">The message</param>
    public NegativeValueException(string message) : base(message)
    {
    }


    /// <summary>
    ///     Initializes a new instance of the <see cref="NegativeValueException" /> class
    /// </summary>
    public NegativeValueException() : base("Value must be non negative ", 400)
    {
    }
}