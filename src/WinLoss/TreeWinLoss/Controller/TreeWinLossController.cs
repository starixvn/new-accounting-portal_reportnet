using Microsoft.AspNetCore.Mvc;
using TreeWinLoss.Criteria;
using TreeWinLoss.Services;

namespace TreeWinLoss.Controller
{
    [Route("api/tree-win-loss")]

    [ApiController]
    public class TreeWinLossController(TreeWinLossService service) : ControllerBase
    {
        [Route("product")]
        public async Task<IActionResult> GetProduct()
        {
            var result = await service.GetProduct();
            return Ok(result);
        }

        [Route("sport")]
        public async Task<IActionResult> GetSport()
        {
            var result = await service.GetSport();
            return Ok(result);
        }

        [Route("main")]
        public async Task<IActionResult> GetTreeWinLossByProduct(TreeWinLossByProductCriteria criteria)
        {
            var result = await service.GetTreeWinLossByProduct(criteria);
            return Ok(result);
        }

        [Route("win-loss-by-level")]
        public async Task<IActionResult> GetTreeWinLossByLevel(TreeWinLossByLevelCriteria criteria)
        {
            var result = await service.GetTreeWinLossByLevel(criteria);
            return Ok(result);
        }
    }
}
