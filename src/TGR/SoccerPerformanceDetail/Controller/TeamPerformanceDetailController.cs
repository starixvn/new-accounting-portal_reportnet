using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;
using SoccerPerformanceDetail.ViewModel;

namespace SoccerPerformanceDetail.Controller;


[Route("api/soccer-performance-detail/team")]
[ApiController]
public class TeamPerformanceDetailController(SoccerPerformanceDetailService service) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetTeamPerformance(GetTeamPerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }
        
        TeamPerformanceList teamPerformance = await service.GetTeamPerformance(criteria);

        return Ok(teamPerformance);
    }

    [Route("team-trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfTeam(GetTraderPerformanceOfTeamCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var traderPerformance = await service.GetTraderPerformanceOfTeam(criteria);
        
        return Ok(traderPerformance);
    }

    [Route("team-league-detail")]
    public async Task<IActionResult> GetLeagueDetailTeam(GetLeagueDetailTeamCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;

        var teamPerformanceLeague = await service.GetLeaguePerformance(criteria);

        return Ok(teamPerformanceLeague);
    }

    [Route("team-trader-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfTeamTrader(GetMatchPerformanceOfTeamTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var matchPerformance = await service.GetMatchPerformanceOfTeamTrader(criteria);
        
        return Ok(matchPerformance);
    }

    [Route("team-trader-match-bettype-detail")]
    public async Task<IActionResult> GetBettypeDetailTeam(GetBettypeDetailTeamCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var bettypeDetail = await service.GetBettypeDetailTeam(criteria);
        
        return Ok(bettypeDetail);
    }

    [Route("team-league-trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfLeagueTeam(GetTraderPerformanceOfLeagueTeam criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetTraderPerformance(criteria);
        
        return Ok(result);
    }

    [Route("team-league-trader-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfTraderLeagueTeam(GetMatchPerformanceOfTraderLeagueTeam criteria)
    {
        criteria.IsExcludeLeagues = true;

        var data = await service.GetMatchPerformanceOfTrader(criteria);

        return Ok(data);
    }
}
