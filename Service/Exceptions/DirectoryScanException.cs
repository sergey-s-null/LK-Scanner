namespace Service.Exceptions;

public class DirectoryScanException : Exception
{
    public DirectoryScanException(string? message) : base(message)
    {
    }

    public DirectoryScanException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}