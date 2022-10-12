using System.Security;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Requests;
using Service.Responses;
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
        DirectoryInfo directoryInfo;
        try
        {
            directoryInfo = new DirectoryInfo(request.Directory);
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
            return new StatusResponse(true, scanResult);
        }
        catch (DirectoryScanException e)
        {
            return new StatusResponse(true, errorMessage: e.Message);
        }
    }
}