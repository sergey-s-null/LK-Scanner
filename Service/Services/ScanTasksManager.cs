using Service.Entities.Abstract;
using Service.Exceptions;
using Service.Services.Abstract;

namespace Service.Services;

public class ScanTasksManager : IScanTasksManager
{
    private readonly IScanService _scanService;

    private readonly IList<Task<IScanResult>> _tasks;

    public ScanTasksManager(IScanService scanService)
    {
        _scanService = scanService;

        _tasks = new List<Task<IScanResult>>();
    }

    public int Start(string directory)
    {
        var task = _scanService.ScanDirectoryAsync(new DirectoryInfo(directory));
        _tasks.Add(task);
        return _tasks.Count - 1;
    }

    public bool Exists(int taskId)
    {
        return taskId >= 0 && taskId < _tasks.Count;
    }

    public bool IsFinished(int taskId)
    {
        if (!Exists(taskId))
        {
            throw new ScanTasksManagerException("Task with specified id does not exists.");
        }

        return _tasks[taskId].IsCompleted;
    }

    public IScanResult GetResult(int taskId)
    {
        if (!IsFinished(taskId))
        {
            throw new ScanTasksManagerException("Task with specified id is not finished.");
        }

        return _tasks[taskId].Result;
    }
}