using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria.JuiceLeftOver;

namespace TGR.Controller.JuiceLeftOver;

[Route("api/juice-left-over/trader")]
public class TraderController(TraderService service) : ControllerBase
{
    [Route("main")]

    public async Task<IActionResult>  GetTraderPerformance(GetTraderPerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var traderPerformance = await service.GetTraderPerformance(criteria);

        return Ok(traderPerformance);
    }

    [Route("trader-match")]
    public async Task<IActionResult>  GetTraderMatchPerformance(GetTraderMatchPerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var traderMatchPerformance = await service.GetTraderMatchPerformance(criteria);

        return Ok(traderMatchPerformance.GroupBy(l => l.LeagueId));
    }

    [Route("trader-match-bettype")]
    public async Task<IActionResult>  GetTraderMatchBettypePerformance(GetTraderMatchBettypePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        
        var traderMatchBettypePerformance = await service.GetTraderMatchBettypePerformance(criteria);

        return Ok(traderMatchBettypePerformance);
    }

    public ActionResult ExportTraderPerformance(GetTraderPerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var isViewScore = true;

        return new EmptyResult();
    }
}
