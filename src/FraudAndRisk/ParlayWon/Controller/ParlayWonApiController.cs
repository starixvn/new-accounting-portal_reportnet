using Microsoft.AspNetCore.Mvc;
using ParlayWon.Criteria;
using ParlayWon.Services;

namespace ParlayWon.Controller
{
    [Route("api/parlay-won")]

    [ApiController]
    public class ParlayWonApiController(ParlayWonService service) : ControllerBase
    {
        [Route("report")]
        public async Task<IActionResult> GetReport(ParlayWonReportCriteria criteria)
        {
            var result = await service.GetReport(criteria);
            return Ok(result);
        }

        [Route("sort")]
        public async Task<IActionResult> SortReport(ParlayWonReportCriteria criteria)
        {
            var result = await service.SortReport(criteria);
            return Ok(result);
        }
    }
}
