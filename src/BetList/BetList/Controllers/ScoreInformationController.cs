using BetList.Criteria;
using BetList.Services;
using Microsoft.AspNetCore.Mvc;

namespace BetList.Controllers
{
    [Route("api/bet-list")]
    [ApiController]
    public class ScoreInformationController : ControllerBase
    {
        private readonly ScoreInformationService service;

        public ScoreInformationController(ScoreInformationService service)
        {
            this.service = service;
        }

        [Route("score")]
        public async Task<IActionResult> GetScoreInformation(GetScoreInformationCriteria criteria)
        {
            var result = await service.GetScoreInfo(criteria);
            return Ok(result);
        }
    }
}
