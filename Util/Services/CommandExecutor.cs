using Core.Requests;
using Core.Responses.Models;
using Util.Services.Abstract;

namespace Util.Services;

public class CommandExecutor : ICommandExecutor
{
    private readonly IRemoteScannerService _remoteScannerService;

    public CommandExecutor(IRemoteScannerService remoteScannerService)
    {
        _remoteScannerService = remoteScannerService;
    }

    public async Task ExecuteAsync(string command, string argument)
    {
        switch (command)
        {
            case "scan":
                await ExecuteScanCommandAsync(argument);
                break;
            case "status":
                await ExecuteStatusCommandAsync(argument);
                break;
            default:
                Console.WriteLine($"Invalid command: {command}");
                break;
        }
    }

    private async Task ExecuteScanCommandAsync(string directory)
    {
        var startScanningResponse = await _remoteScannerService.StartScanningAsync(new StartScanningRequest(directory));
        if (startScanningResponse.ErrorMessage is not null)
        {
            Console.WriteLine($"Error on start task: {startScanningResponse.ErrorMessage}");
            return;
        }

        if (!startScanningResponse.IsStarted)
        {
            Console.WriteLine("Task is not started, but there is no error message in response. &@?%!#");
            return;
        }

        if (startScanningResponse.StartedTaskId is null)
        {
            Console.WriteLine("Task started, no error message, but task id is null. ???");
            return;
        }

        Console.WriteLine($"Task started. Id: {startScanningResponse.StartedTaskId}");
    }

    private async Task ExecuteStatusCommandAsync(string taskIdStr)
    {
        if (!int.TryParse(taskIdStr, out var taskId))
        {
            Console.WriteLine("Could not parse task id.");
            return;
        }

        var statusResponse = await _remoteScannerService.GetScanningStatus(new StatusRequest(taskId));

        if (!statusResponse.IsFinished)
        {
            Console.WriteLine("Task is in progress.");
            return;
        }

        if (statusResponse.ErrorMessage is not null)
        {
            Console.WriteLine($"Task finished with error: {statusResponse.ErrorMessage}");
            return;
        }

        if (statusResponse.ScanResult is null)
        {
            Console.WriteLine("Task finished without error, but there is no scan result in response. &*()))))))");
            return;
        }

        DisplayScanResult(statusResponse.ScanResult);
    }

    private static void DisplayScanResult(ScanResultModel scanResult)
    {
        Console.WriteLine("====== Scan result ======");

        Console.WriteLine($"Directory: {scanResult.DirectoryPath}");

        Console.WriteLine($"Processed files: {scanResult.Processed}");

        foreach (var detection in scanResult.Detections)
        {
            Console.WriteLine($"{detection.SuspicionName} detections: {detection.SuspicionsCount}");
        }

        Console.WriteLine($"Errors: {scanResult.Errors}");

        Console.WriteLine($"Execution time: {scanResult.ExecutionTime}");

        Console.WriteLine("=========================");
    }
}