using Microsoft.AspNetCore.Mvc;
using SuperBalance.Criteria;
using SuperBalance.Service;

namespace SuperBalance.Controller
{
    [Route("api/super-balance")]
    [ApiController]
    public class SuperBalanceApiController(SuperBalanceService service) : ControllerBase
    {
        [Route("super")]
        public async Task<IActionResult> GetSuperBalance(SuperBalanceCriteria criteria)
        {
            var result = await service.GetSuperBalance(criteria);
            return Ok(result);
        }

        [Route("super-downline")]
        public async Task<IActionResult> GetSuperBalanceDownLine(SuperDownlineCriteria criteria)
        {
            var result = await service.GetSuperBalanceDownLine(criteria);
            return Ok(result);
        }

        [Route("master-downline")]
        public async Task<IActionResult> GetMasterBalanceDownLine(MasterDownlineCriteria criteria)
        {
            var result = await service.GetMasterDownLine(criteria);
            return Ok(result);
        }

        [Route("agent-downline")]
        public async Task<IActionResult> GetAgentBalanceDownLine(AgentDownlineCriteria criteria)
        {
            var result = await service.GetAgentDownLine(criteria);
            return Ok(result);
        }

        [Route("currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            var result = await service.GetCurrencies();
            return Ok(result);
        }

        [Route("alphabet")]
        public IActionResult GetListAlphabet()
        {
            var result = service.GetListAlphabet();
            return Ok(result);
        }
    }
}
