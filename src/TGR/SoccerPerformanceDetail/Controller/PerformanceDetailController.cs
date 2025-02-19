namespace SoccerPerformanceDetail.Controller;

using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;

public class PerformanceDetailController(SoccerPerformanceDetailService soccerDetailService) : ControllerBase
{
    public async Task<IActionResult> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria)
    {
        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }

        var betTypePerformance = await soccerDetailService.GetBetTypePerformance(criteria);

        return Ok(betTypePerformance);
    }
}