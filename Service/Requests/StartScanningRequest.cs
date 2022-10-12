namespace Service.Requests;

public class StartScanningRequest
{
    public string Directory { get; }

    public StartScanningRequest(string directory)
    {
        Directory = directory;
    }
}