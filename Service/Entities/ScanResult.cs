using Service.Entities.Abstract;

namespace Service.Entities;

public class ScanResult : IScanResult
{
    public int Processed { get; }
    public IReadOnlyDictionary<SuspicionType, int> Detects { get; }
    public int Errors { get; }
    public TimeSpan ExecutionTime { get; }

    public ScanResult(
        int processed,
        IReadOnlyDictionary<SuspicionType, int> detects,
        int errors,
        TimeSpan executionTime)
    {
        Processed = processed;
        Detects = detects;
        Errors = errors;
        ExecutionTime = executionTime;
    }
}