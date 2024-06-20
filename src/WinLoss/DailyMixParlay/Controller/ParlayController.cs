using DailyMixParlay.Criteria;
using DailyMixParlayWinLoss.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailyMixParlay.Controller
{
    [Route("api/parlay")]

    [ApiController]
    public class ParlayController : ControllerBase
    {
        private readonly DailyMixParlayWinLossService service;

        public ParlayController(DailyMixParlayWinLossService service)
        {
            this.service = service;
        }

        [Route("combination")]
        public async Task<IActionResult> GetParlayCombination(GetCombinationWinLossCriteria criteria)
        {
            var result = await service.GetCombinationWinLossReport(criteria);
            return Ok(result);
        }

        [Route("product")]
        public async Task<IActionResult> GetParlayProduct(GetAdvanceMixParlayCriteria criteria)
        {
            var result = await service.GetAdvanceMixParlayReport(criteria);
            return Ok(result);
        }
    }
}
