namespace Util.Exceptions;

public class RemoteServiceCallException : Exception
{
    public RemoteServiceCallException(string? message) : base(message)
    {
    }

    public RemoteServiceCallException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}