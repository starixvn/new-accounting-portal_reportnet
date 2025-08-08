using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria.JuiceLeftOver;

namespace TGR.Controller.JuiceLeftOver;

[Route("api/juice-left-over/bettype")]
public class BetTypeController(BetTypeService service) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetBetTypePerformance(criteria);

        return Ok(result);
    }

    [Route("bettype-trader")]
    public async Task<IActionResult> GetTraderPerformanceOfBetType(GetTraderPerformanceOfBetTypeCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetBetTypeTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("bettype-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfBetTypeTrader(GetMatchPerformanceOfBetTypeTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetBetTypeTraderMatchPerformance(criteria);

        return Ok(result);
    }

    [Route("bettype-league")]
    public async Task<IActionResult> GetLeaguePerformanceOfBetType(GetLeaguePerformanceOfBetTypeCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetBetTypeLeaguePerformance(criteria);

        return Ok(result);
    }

    [Route("bettype-league-trader")]
    public async Task<IActionResult> GetTraderPerformanceOfBetTypeLeague(GetTraderPerformanceOfBetTypeLeagueCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetBetTypeLeagueTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("bettype-league-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfBetTypeLeagueTrader(GetMatchPerformanceOfBetTypeLeagueTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetBetTypeLeagueTraderMatchPerformance(criteria);

        return Ok(result);
    }

    public ActionResult ExportBetTypePerformance(GetBetTypePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;

        return new EmptyResult();
    }
}
