using Margin.Criteria;
using Margin.Services;
using Microsoft.AspNetCore.Mvc;

namespace Margin.Controller
{
    [Route("api/margin-win-loss")]
    [ApiController]
    public class MarginWinLossApiController(MarginWinLossService service) : ControllerBase
    {
        [Route("data")]
        public async Task<IActionResult> GetDataMarginWinLoss(MarginWinLossCriteria criteria)
        {
            if (criteria == null)
            {
                criteria = new MarginWinLossCriteria();
            }
            
            var result = await service.GetMarginWinLoss(criteria);

            return Ok(result);
        }
    }
}
