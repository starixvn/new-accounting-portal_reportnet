using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;
using SoccerPerformanceDetail.ViewModel;

namespace SoccerPerformanceDetail.Controller;

[Route("api/soccer-performance-detail")]
[ApiController]
public class LeaguePerformanceDetailController(SoccerPerformanceDetailService service) : ControllerBase
{
    public async Task<IActionResult> GetLeaguePerformance(GetLeaguePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.LeagueName = criteria.LeagueName ?? string.Empty;

        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }

        var leaguePerformance = service.GetLeaguePerformance(criteria, criteria.MarketFilter == Market.All);

        return Ok(leaguePerformance);
    }

    public async Task<IActionResult> GetTraderPerformanceOfLeague(GetTraderPerformanceOfLeagueCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var traderPerformance = service.GetTraderPerformanceOfLeague(criteria);

        return Ok(traderPerformance);
    }

    public async Task<IActionResult> GetMatchPerformanceOfLeagueTrader(GetMatchPerformanceOfLeagueTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var matchPerformance = service.GetMatchPerformanceOfLeagueTrader(criteria);

        return Ok(matchPerformance);
    }

    public async Task<IActionResult> GetBettypeDetailLeague(GetBettypeDetailLeague criteria)
    {
        criteria.IsExcludeLeagues = true;
       
        var bettypeDetail = service.GetBettypeDetailLeague(criteria);
        
        return Ok(bettypeDetail);
    }
}
