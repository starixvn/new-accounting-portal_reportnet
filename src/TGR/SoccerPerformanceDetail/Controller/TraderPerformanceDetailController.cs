using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;
using SoccerPerformanceDetail.ViewModel;

namespace SoccerPerformanceDetail.Controller
{
    public class TraderPerformanceDetailController(SoccerPerformanceDetailService service) : ControllerBase
    {
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

        public async Task<IActionResult> GetMatchPerformanceOfTrader(GetMatchPerformanceOfTraderCriteria criteria)
        {
            criteria.IsExcludeLeagues = true;
            MatchPerformanceList martchPerformance = await service.GetMatchPerformanceOfTrader(criteria);
           
            return Ok(martchPerformance);
        }

        public async Task<IActionResult> GetBettypeDetailTrader(GetBettypeDetailTrader criteria)
        {
            criteria.IsExcludeLeagues = true;
            var bettypeDetail = await service.GetBettypeDetailTrader(criteria);
            
            return Ok(bettypeDetail);
        }
    }
}
