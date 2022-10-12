namespace Service.Responses.Models;

public class SuspicionDetectionModel
{
    public string SuspicionName { get; }
    public int SuspicionsCount { get; }

    public SuspicionDetectionModel(string suspicionName, int suspicionsCount)
    {
        SuspicionName = suspicionName;
        SuspicionsCount = suspicionsCount;
    }
}