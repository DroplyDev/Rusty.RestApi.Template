namespace Rusty.Template.Contracts.Exceptions;

public class OrderParamNameNotValidException : ApiException
{
    public OrderParamNameNotValidException(string message, string field) : base(message)
    {
        Field = field;
    }

    public OrderParamNameNotValidException(string field) : base($"field: {field} was not found in database", 400)
    {
        Field = field;
    }

    public string Field { get; }
}