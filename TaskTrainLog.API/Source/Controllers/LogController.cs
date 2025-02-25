using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TT.Core;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

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

    [HttpGet]
    public async Task<IActionResult> ConsumeMessagesRabbitMQ()
    {
        return Ok();
    }
}
