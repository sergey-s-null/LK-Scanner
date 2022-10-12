using Service.Entities.Abstract;

namespace Service.Responses;

public class StatusResponse
{
    public bool IsFinished { get; }
    public IScanResult? ScanResult { get; }

    public StatusResponse(bool isFinished, IScanResult? scanResult = null)
    {
        IsFinished = isFinished;
        ScanResult = scanResult;
    }
}