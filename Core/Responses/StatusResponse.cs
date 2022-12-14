using Core.Responses.Models;

namespace Core.Responses;

public class StatusResponse
{
    public bool IsFinished { get; }
    public ScanResultModel? ScanResult { get; }
    public string? ErrorMessage { get; }

    public StatusResponse(
        bool isFinished,
        ScanResultModel? scanResult = null,
        string? errorMessage = null)
    {
        IsFinished = isFinished;
        ErrorMessage = errorMessage;
        ScanResult = scanResult;
    }
}