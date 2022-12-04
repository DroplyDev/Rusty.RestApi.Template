namespace Rusty.Template.Contracts.Exceptions;

public class NegativeValueException : ApiException
{
    public NegativeValueException(string message) : base(message)
    {
    }


    public NegativeValueException() : base("Value must be non negative ", 400)
    {
    }
}