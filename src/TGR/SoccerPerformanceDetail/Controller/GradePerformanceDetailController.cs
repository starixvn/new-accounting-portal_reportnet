using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;

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
}
