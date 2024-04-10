using Microsoft.AspNetCore.Mvc;

namespace Utilities.ServiceTime
{
    [ApiController]
    public class ServerTimeController : ControllerBase
    {
        [Route("state-api/application/today")]
        [HttpPost]
        public IActionResult Today()
        {
            return Ok(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day));
        }

        [Route("state-api/application/server-time")]
        [HttpPost]
        public IActionResult ServerTime()
        {
            return Ok(DateTime.Now);
        }
    }
}
