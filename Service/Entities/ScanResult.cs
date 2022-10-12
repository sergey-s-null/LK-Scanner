using Service.Entities.Abstract;

namespace Service.Entities;

public class ScanResult : IScanResult
{
    public string DirectoryPath { get; }
    public int Processed { get; }
    public IReadOnlyDictionary<SuspicionType, int> Detections { get; }
    public int Errors { get; }
    public TimeSpan ExecutionTime { get; }

    public ScanResult(
        string directoryPath,
        int processed,
        IReadOnlyDictionary<SuspicionType, int> detections,
        int errors,
        TimeSpan executionTime)
    {
        DirectoryPath = directoryPath;
        Processed = processed;
        Detections = detections;
        Errors = errors;
        ExecutionTime = executionTime;
    }
}