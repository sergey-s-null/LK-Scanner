using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SomeController
{
    [HttpGet]
    public ActionResult Scan()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public ActionResult Status()
    {
        throw new NotImplementedException();
    }
}