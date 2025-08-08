using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria.JuiceLeftOver;

namespace TGR.Controller.JuiceLeftOver;

[Route("api/juice-left-over/team")]
public class TeamController(TeamService service) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetTeamPerformance(GetTeamPerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetTeamPerformance(criteria);

        return Ok(result);
    }

    [Route("team-trader")]
    public async Task<IActionResult> GetTraderPerformanceOfTeam(GetTraderPerformanceOfTeamCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetTeamTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("team-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfTeamTrader(GetMatchPerformanceOfTeamTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetTeamTraderMatchPerformance(criteria);

        return Ok(result);
    }

    public async Task<IActionResult> ExportTeamPerformance(GetTeamPerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var isViewScore = true;


        return new EmptyResult();
    }

    [Route("team-league-trader-match-bettype")]
    public async Task<IActionResult> GetBettypeDetailTeam(GetBettypeDetailTeamCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetTeamLeagueTraderMatchBettypePerformance(criteria);

        return Ok(result);
    }

    [Route("team-league")]
    public async Task<IActionResult> GetLeagueDetailTeam(GetLeagueDetailTeamCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetTeamLeaguePerformance(criteria);

        return Ok(result);
    }

    [Route("team-league-trader")]
    public async Task<IActionResult> GetTraderPerformanceOfLeagueTeam(GetTraderPerformanceOfLeagueTeam criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetTeamLeagueTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("team-league-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfTraderLeagueTeam(GetMatchPerformanceOfTraderLeagueTeamCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetTeamLeagueTraderMatchPerformance(criteria);

        return Ok(result);
    }
}
