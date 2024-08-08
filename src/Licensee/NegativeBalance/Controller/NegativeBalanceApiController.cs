using Microsoft.AspNetCore.Mvc;
using NegativeBalance.Criteria;
using NegativeBalance.Services;

namespace NegativeBalance.Controller
{
    [Route("api/negative-balance")]

    [ApiController]

    public class NegativeBalanceApiController(NegativeBalanceService service) : ControllerBase
    {
        [Route("data")]
        public async Task<IActionResult> GetNegativeBalance(NegativeBalanceCriteria criteria)
        {
            var result = await service.GetNegativeBalance(criteria);
            return Ok(result);
        }
    }
}