using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria.JuiceLeftOver;

namespace TGR.Controller.JuiceLeftOver;

[Route("api/juice-left-over/league")]
public class LeagueController(LeagueService service) : ControllerBase
{
    [Route("api/juice-left-over/main")]
    public async Task<IActionResult> GetLeaguePerformance(GetLeaguePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetLeaguePerformance(criteria);

        return Ok(result);
    }

    public ActionResult ExportLeaguePerformance(GetLeaguePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;

        return new EmptyResult();
    }

    public ActionResult ExportMatchPerformance(GetMatchPerformanceOfLeagueNameCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        return new EmptyResult();
    }

    [Route("league-trader")]
    public async Task<IActionResult> GetTraderPerformanceOfLeague(GetTraderPerformanceOfLeagueCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetTraderPerformanceByLeague(criteria);

        return Ok(result);
    }

    [Route("league-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfLeagueTrader(GetMatchPerformanceOfLeagueTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetMatchPerformanceOfLeagueAndTrader(criteria);

        return Ok(result);
    }

    [Route("league-bettype")]
    public async Task<IActionResult> GetBettypeDetailLeague(GetBettypeDetailLeagueCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetBettypePerformanceOfLeagueTraderAndMatch(criteria);

        return Ok(result);
    }

    [Route("league-match")]
    public async Task<IActionResult> GetMatchPerformanceOfLeague(GetMatchPerformanceOfLeagueCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetMatchPerformanceOfLeague(criteria);

        return Ok(result);
    }

    [Route("league-match-bettype")]
    public async Task<IActionResult> GetBettypePerformanceOfLeagueMatch(GetBetTypePerformanceOfLeagueMatchCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetBettypePerformanceOfLeagueAndMatch(criteria);

        return Ok(result);
    }
}
