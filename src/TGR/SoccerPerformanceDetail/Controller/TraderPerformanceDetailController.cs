using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;
using SoccerPerformanceDetail.ViewModel;

namespace SoccerPerformanceDetail.Controller
{
    [Route("api/soccer-performance-detail/trader")]
    [ApiController]
    public class TraderPerformanceDetailController(SoccerPerformanceDetailService service) : ControllerBase
    {
        [Route("main")]
        public async Task<IActionResult> GetTraderPerformance(GetTraderPerformanceCriteria criteria)
        {
            TraderPerformanceList traderPerformance = null;

            criteria.IsExcludeLeagues = true;
            
            criteria.Username = criteria.Username ?? string.Empty;
            
            if (criteria.IsAllLeagueGroup)
            {
                criteria.LeagueGroupIds = string.Empty;
            }
           
            traderPerformance = await service.GetTraderPerformance(criteria);
            
            return Ok(traderPerformance);
        }

        [Route("trader-match-detail")]
        public async Task<IActionResult> GetMatchPerformanceOfTrader(GetMatchPerformanceOfTraderCriteria criteria)
        {
            criteria.IsExcludeLeagues = true;
            MatchPerformanceList martchPerformance = await service.GetMatchPerformanceOfTrader(criteria);
           
            return Ok(martchPerformance);
        }

        [Route("trader-bettype-detail")]
        public async Task<IActionResult> GetBettypeDetailTrader(GetBettypeDetailTrader criteria)
        {
            criteria.IsExcludeLeagues = true;
            var bettypeDetail = await service.GetBettypeDetailTrader(criteria);
            
            return Ok(bettypeDetail);
        }
    }
}
