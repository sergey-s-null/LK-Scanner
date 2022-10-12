namespace Core.Responses.Models;

public class ScanResultModel
{
    public int Processed { get; }
    public IReadOnlyCollection<SuspicionDetectionModel> Detections { get; }
    public int Errors { get; }
    public TimeSpan ExecutionTime { get; }

    public ScanResultModel(
        int processed,
        IReadOnlyCollection<SuspicionDetectionModel> detections,
        int errors,
        TimeSpan executionTime)
    {
        Processed = processed;
        Detections = detections;
        Errors = errors;
        ExecutionTime = executionTime;
    }
}