using System.Security;
using Core.Requests;
using Core.Responses;
using Core.Responses.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Entities.Abstract;
using Service.Exceptions;
using Service.Services.Abstract;

namespace Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ScannerController : ControllerBase
{
    private readonly IScanTasksManager _scanTasksManager;

    public ScannerController(IScanTasksManager scanTasksManager)
    {
        _scanTasksManager = scanTasksManager;
    }

    [HttpPost]
    public ActionResult<StartScanningResponse> StartScanning(StartScanningRequest request)
    {
        var directoryExpanded = Environment.ExpandEnvironmentVariables(request.Directory);
        DirectoryInfo directoryInfo;
        try
        {
            directoryInfo = new DirectoryInfo(directoryExpanded);
        }
        catch (SecurityException e)
        {
            return new StartScanningResponse(false, errorMessage: e.Message);
        }
        catch (ArgumentException e)
        {
            return new StartScanningResponse(false, errorMessage: e.Message);
        }
        catch (PathTooLongException e)
        {
            return new StartScanningResponse(false, errorMessage: e.Message);
        }

        var taskId = _scanTasksManager.Start(directoryInfo);
        return new StartScanningResponse(true, taskId);
    }

    [HttpPost]
    public async Task<ActionResult<StatusResponse>> Status(StatusRequest request)
    {
        if (!_scanTasksManager.Exists(request.TaskId))
        {
            return NotFound();
        }

        if (!_scanTasksManager.IsFinished(request.TaskId))
        {
            return new StatusResponse(false);
        }

        try
        {
            var scanResult = await _scanTasksManager.GetResultAsync(request.TaskId);
            var scanResultModel = MapScanResult(scanResult);
            return new StatusResponse(true, scanResultModel);
        }
        catch (DirectoryScanException e)
        {
            return new StatusResponse(true, errorMessage: e.Message);
        }
    }

    private static ScanResultModel MapScanResult(IScanResult scanResult)
    {
        return new ScanResultModel(
            scanResult.DirectoryPath,
            scanResult.Processed,
            scanResult.Detections
                .Select(x => new SuspicionDetectionModel(x.Key.Name, x.Value))
                .ToList(),
            scanResult.Errors,
            scanResult.ExecutionTime
        );
    }
}