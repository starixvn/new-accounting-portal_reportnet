using Microsoft.AspNetCore.Mvc;
using PeakConcurrentUsers.Criteria;
using PeakConcurrentUsers.Services;

namespace PeakConcurrentUsers.Controller
{
    [Route("api/peak-concurrent-user")]

    [ApiController]

    public class PeakConcurrentUsersApiController(PeakConcurrentUserService service) : ControllerBase
    {
        [Route("data")]
        public async Task<IActionResult> GetPeakConcurrentUsers(PeakConcurrentUserCriteria criteria)
        {
            var result = await service.GetConcurrentUserList(criteria);
            return Ok(result);
        }
    }
}
