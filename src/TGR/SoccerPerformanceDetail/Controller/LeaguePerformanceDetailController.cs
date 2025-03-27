using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;

namespace SoccerPerformanceDetail.Controller;

[Route("api/soccer-performance-detail/league")]
[ApiController]
public class LeaguePerformanceDetailController(SoccerPerformanceDetailService service) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetLeaguePerformance(GetLeaguePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.LeagueName = criteria.LeagueName ?? string.Empty;

        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }

        var leaguePerformance = await service.GetLeaguePerformance(criteria, criteria.MarketFilter == Market.All);

        return Ok(leaguePerformance);
    }

    [Route("league-trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfLeague(GetTraderPerformanceOfLeagueCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var traderPerformance = await service.GetTraderPerformanceOfLeague(criteria);

        return Ok(traderPerformance);
    }

    [Route("league-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfLeagueTrader(GetMatchPerformanceOfLeagueTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var matchPerformance = await service.GetMatchPerformanceOfLeagueTrader(criteria);

        return Ok(matchPerformance);
    }

    [Route("league-bettype-detail")]
    public async Task<IActionResult> GetBettypeDetailLeague(GetBettypeDetailLeague criteria)
    {
        criteria.IsExcludeLeagues = true;
       
        var bettypeDetail = await service.GetBettypeDetailLeague(criteria);
        
        return Ok(bettypeDetail);
    }
}
