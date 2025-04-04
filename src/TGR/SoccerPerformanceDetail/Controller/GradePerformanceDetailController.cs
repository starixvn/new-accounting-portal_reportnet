using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;
using SoccerPerformanceDetail.ViewModel;

namespace SoccerPerformanceDetail.Controller;


[Route("api/soccer-performance-detail/grade")]
[ApiController]
public class GradePerformanceDetailController(SoccerPerformanceDetailService service) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetGradePerformance(GetGradePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;

        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }

        var gradePerformance = await service.GetGradePerformance(criteria);
        
        return Ok(gradePerformance);
    }

    [Route("trader-trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfGrade(GetTraderPerformanceOfGradeCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var traderPerformance = await service.GetTraderPerformanceOfGrade(criteria);

        return Ok(traderPerformance);
    }

    [Route("trader-league-detail")]
    public async Task<IActionResult> GetLeaguePerformanceOfGrade(GetLeaguePerformanceOfGradeCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var leaguePerformance = await service.GetLeaguePerformanceOfGrade(criteria);

        return Ok(leaguePerformance);
    }

    [Route("trader-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfGradeTrader(GetMatchPerformanceOfGradeTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var matchPerformance = await service.GetMatchPerformanceOfGradeTrader(criteria);

        return Ok(matchPerformance);
    }

    [Route("trader-bettype-detail")]
    public async Task<IActionResult> GetBettypeDetailGrade(GetBettypeDetailGradeCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var bettypeDetail = await service.GetBettypeDetailGrade(criteria);

        return Ok(bettypeDetail);
    }

    [Route("trader-by-league-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfGradeByLeague(GetTraderPerformanceOfGradeByLeagueCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var leaguePerformance = await service.GetTraderPerformanceOfGradeByLeague(criteria);

        return Ok(leaguePerformance);
    }
}
