namespace Core.Responses.Models;

public class ScanResultModel
{
    public string DirectoryPath { get; }
    public int Processed { get; }
    public IReadOnlyCollection<SuspicionDetectionModel> Detections { get; }
    public int Errors { get; }
    public TimeSpan ExecutionTime { get; }

    public ScanResultModel(
        string directoryPath,
        int processed,
        IReadOnlyCollection<SuspicionDetectionModel> detections,
        int errors,
        TimeSpan executionTime)
    {
        Processed = processed;
        Detections = detections;
        Errors = errors;
        ExecutionTime = executionTime;
        DirectoryPath = directoryPath;
    }
}