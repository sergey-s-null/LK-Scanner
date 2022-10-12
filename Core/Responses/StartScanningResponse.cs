namespace Core.Responses;

public class StartScanningResponse
{
    public bool IsStarted { get; }
    public int? StartedTaskId { get; }
    public string? ErrorMessage { get; }

    public StartScanningResponse(bool isStarted, int? startedTaskId = null, string errorMessage = null)
    {
        IsStarted = isStarted;
        StartedTaskId = startedTaskId;
        ErrorMessage = errorMessage;
    }
}