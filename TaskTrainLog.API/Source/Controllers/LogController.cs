using Microsoft.AspNetCore.Mvc;

namespace TT.Log;

[ApiController]
[Route("[controller]/[action]")]
public class LogController : ControllerBase
{
    public LogController()
    {
    }

    [HttpGet]
    public ActionResult GetTrackingServices()
    {
        return Ok();
    }
}
