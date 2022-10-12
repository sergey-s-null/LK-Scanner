namespace Service.Exceptions;

public class ScanTasksManagerException : Exception
{
    public ScanTasksManagerException(string? message) : base(message)
    {
    }

    public ScanTasksManagerException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}