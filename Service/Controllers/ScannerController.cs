using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public ActionResult<int> Scan(string directory)
    {
        return _scanTasksManager.Start(directory);
    }

    [HttpGet]
    public ActionResult<StatusResponse> Status(int id)
    {
        if (!_scanTasksManager.Exists(id))
        {
            NotFound();
        }

        if (!_scanTasksManager.IsFinished(id))
        {
            return new StatusResponse(false);
        }

        var scanResult = _scanTasksManager.GetResult(id);
        return new StatusResponse(true, scanResult);
    }
}