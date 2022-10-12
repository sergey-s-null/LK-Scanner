namespace Service.Exceptions;

public class FileScanException : Exception
{
    public FileScanException(string? message) : base(message)
    {
    }

    public FileScanException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}