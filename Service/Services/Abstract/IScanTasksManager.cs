using Service.Entities.Abstract;
using Service.Exceptions;

namespace Service.Services.Abstract;

public interface IScanTasksManager
{
    /// <returns>Id of started task.</returns>
    int Start(string directory);

    bool Exists(int taskId);

    /// <exception cref="ScanTasksManagerException">Task does not exists.</exception>
    bool IsFinished(int taskId);

    /// <exception cref="ScanTasksManagerException">Task does not exists or not finished.</exception>
    /// <exception cref="DirectoryScanException">Error occured on directory scan.</exception>
    IScanResult GetResult(int taskId);
}