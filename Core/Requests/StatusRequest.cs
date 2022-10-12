namespace Core.Requests;

public class StatusRequest
{
    public int TaskId { get; }

    public StatusRequest(int taskId)
    {
        TaskId = taskId;
    }
}