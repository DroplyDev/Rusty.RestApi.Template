namespace Rusty.Template.Contracts.Exceptions;

public class InsufficientPrivilegeException : ApiException
{
    protected InsufficientPrivilegeException(string message) : base(message, 403)
    {
    }

    public InsufficientPrivilegeException() : base("Permission denied", 403)
    {
    }
}