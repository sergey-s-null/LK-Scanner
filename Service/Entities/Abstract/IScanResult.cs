namespace Service.Entities.Abstract;

public interface IScanResult
{
    string DirectoryPath { get; }
    int Processed { get; }
    IReadOnlyDictionary<SuspicionType, int> Detections { get; }
    int Errors { get; }
    TimeSpan ExecutionTime { get; }
}