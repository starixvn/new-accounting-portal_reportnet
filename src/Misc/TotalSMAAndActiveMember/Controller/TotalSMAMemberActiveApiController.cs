using Microsoft.AspNetCore.Mvc;
using TotalSMAAndActiveMember.Criteria;
using TotalSMAAndActiveMember.Model;
using TotalSMAAndActiveMember.Services;

namespace TotalSMAAndActiveMember.Controller
{
    [Route("api/total-sma-active-member")]
    [ApiController]
    public class TotalSMAMemberActiveApiController(TotalSMAMemberActiveService service) : ControllerBase
    {
        [Route("sites")]
        public async Task<IActionResult> GetSites()
        {
            var result = await service.GetSites();

            return Ok(result);
        }

        [Route("summary")]
        public async Task<IActionResult> GetSummaryReport(TotalSMAMemberActiveCriteria criteria)
        {
            var result = await service.GetReport(criteria);

            return Ok(result);
        }

        [Route("detail")]
        public async Task<IActionResult> GetDetailReport(StatusDetailsCriteria criteria)
        {
            var result = await service.GetStatusDetails(criteria);

            return Ok(new StatusDetailViewModel { Report = result, TotalRow = criteria.TotalRow });
        }
    }
}
