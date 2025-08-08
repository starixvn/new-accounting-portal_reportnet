using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria.JuiceLeftOver;

namespace TGR.Controller.JuiceLeftOver;

[Route("api/juice-left-over/grade")]
public class GradeController(GradeService service) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult>  GetGradePerformance(GetGradePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetGradePerformance(criteria);

        return Ok(result);
    }

    [Route("grade-trader")]
    public async Task<IActionResult>  GetTraderPerformanceOfGrade(GetTraderPerformanceOfGradeCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetGradeTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("grade-league-trader")]
    public async Task<IActionResult>  GetTraderPerformanceOfGradeByLeague(GetTraderPerformanceOfGradeByLeagueCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetGradeLeagueTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("grade-league")]
    public async Task<IActionResult>  GetLeaguePerformanceOfGrade(GetLeaguePerformanceOfGradeCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetGradeLeaguePerformance(criteria);

        return Ok(result);
    }

    [Route("grade-trader-match")]
    public async Task<IActionResult>  GetMatchPerformanceOfGradeTrader(GetMatchPerformanceOfGradeTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetGradeTraderMatchPerformance(criteria);

        return Ok(result);
    }

    [Route("grade-league-trader-match")]
    public async Task<IActionResult>  GetMatchPerformanceOfGradeLeagueTrader(GetMatchPerformanceOfGradeLeagueTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var result = await service.GetGradeLeagueTraderMatchPerformance(criteria);

        return Ok(result);
    }

    [Route("grade-bettype")]
    public async Task<IActionResult>  GetBettypeDetailGrade(GetBetTypePerformanceOfGradeLeagueTraderMatchCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.IsViewScore = true;
        var result = await service.GetGradeLeagueTraderMatchBettypePerformance(criteria);

        return Ok(result);
    }

    public ActionResult ExportGradePerformance(GetGradePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var isViewScore = true;

        return new EmptyResult();
    }
}
