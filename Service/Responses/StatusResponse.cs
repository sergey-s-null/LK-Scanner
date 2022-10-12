using Service.Entities.Abstract;

namespace Service.Responses;

public class StatusResponse
{
    public bool IsFinished { get; }
    public IScanResult? ScanResult { get; }
    public string? ErrorMessage { get; }

    public StatusResponse(bool isFinished, IScanResult? scanResult = null, string? errorMessage = null)
    {
        IsFinished = isFinished;
        ErrorMessage = errorMessage;
        ScanResult = scanResult;
    }
}