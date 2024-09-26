using BetCount.Criteria;
using BetCount.Services;
using Microsoft.AspNetCore.Mvc;

namespace BetCount.Controller
{
    [Route("api/bet-count")]
    [ApiController]
    public class BetCountApiController(BetCountService service) : ControllerBase
    {

        [Route("main")]
        public async Task<IActionResult> GetReport(BetCountCriteria criteria)
        {
            var result = await service.GetReport(criteria);

            return Ok(result);
        }

        [Route("sort")]
        public async Task<IActionResult> SortReport(BetCountCriteria criteria)
        {
            var result = await service.SortReport(criteria);

            return Ok(result);
        }


        [Route("product")]
        public async Task<IActionResult> GetProduct(ProductCriteria criteria)
        {
            var result = await service.GetProduct(criteria);

            return Ok(result);
        }



        [Route("sports")]
        public async Task<IActionResult> GetSport(ProductCriteria criteria)
        {
            var result = await service.GetSport(criteria);

            return Ok(result);
        }
    }
}
