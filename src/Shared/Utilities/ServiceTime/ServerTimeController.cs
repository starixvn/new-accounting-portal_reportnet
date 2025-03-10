namespace Utilities.ServiceTime;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("state-api/application")]
public class ServerTimeController : ControllerBase
{
    [Route("today")]
    [HttpPost]
    public IActionResult Today()
    {
        return Ok(DateTime.Today);
    }

    [Route("server-time")]
    [HttpPost]
    public IActionResult ServerTime()
    {
        return Ok(DateTimeOffset.Now);
    }
}