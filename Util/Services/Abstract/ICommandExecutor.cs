namespace Util.Services.Abstract;

public interface ICommandExecutor
{
    Task ExecuteAsync(string command, string argument);
}