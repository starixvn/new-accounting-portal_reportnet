using DirectBalance.Criteria;
using DirectBalance.Service;
using Microsoft.AspNetCore.Mvc;

namespace DirectBalance.Controller
{
    [Route("api/direct-balance")]
    [ApiController]
    public class DirectBalanceApiController(DirectBalanceService service) : ControllerBase
    {
        [Route("main")]
        public async Task<IActionResult> GetDirectBalance(DirectBalanceCriteria criteria)
        {
            var result = await service.GetDirectBalance(criteria);
            
            return Ok(result);
        }

        [Route("master-downline")]
        public async Task<IActionResult> GetMasterDownline(MasterDownlineCriteria criteria)
        {
            var result = await service.GetDirectBalanceMasterDownLine(criteria);

            return Ok(result);
        }

        [Route("agent-downline")]
        public async Task<IActionResult> GetDirectBalanceAgentDownline(AgentDownlineCriteria criteria)
        {
            var result = await service.GetDirectBalanceAgentDownLine(criteria);

            return Ok(result);
        }

        [Route("currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            var result = await service.GetCurrencies();
            return Ok(result);
        }
    }
}
