namespace Service.Entities.Abstract;

public interface IScanResult
{
    int Processed { get; }
    IReadOnlyDictionary<SuspicionType, int> Detects { get; }
    int Errors { get; }
    TimeSpan ExecutionTime { get; }
}