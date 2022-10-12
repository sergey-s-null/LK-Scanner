using Service.Entities.Abstract;

namespace Service.Services.Abstract;

public interface IScanTasksManager
{
    int Start(string directory);

    bool Exists(int id);

    bool IsFinished(int id);

    IScanResult GetResult(int id);
}