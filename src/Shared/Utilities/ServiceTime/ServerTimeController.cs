using Microsoft.AspNetCore.Mvc;

namespace Utilities.ServiceTime;

[ApiController]
[Route("state-api/application")]
public class ServerTimeController : ControllerBase
{
    [Route("today")]
    [HttpPost]
    public IActionResult Today()
    {
        return Ok(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day));
    }

    [Route("server-time")]
    [HttpPost]
    public IActionResult ServerTime()
    {
        return Ok(DateTime.Now);
    }
}