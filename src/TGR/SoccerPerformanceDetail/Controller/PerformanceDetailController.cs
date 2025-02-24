using Fanex.NAP.Client;
using SoccerPerformanceDetail.Model;

namespace SoccerPerformanceDetail.Controller;

using Microsoft.AspNetCore.Mvc;
using Criteria;
using Services;

[Route("api/soccer-performance-detail")]
[ApiController]
public class PerformanceDetailController(SoccerPerformanceDetailService soccerDetailService,
    INapSession napSession) : ControllerBase
{
    [Route("bettypes")]
    public async Task<IActionResult> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria)
    {
        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }

        var betTypePerformance = await soccerDetailService.GetBetTypePerformance(criteria);

        return Ok(betTypePerformance);
    }

    [Route("default-bettypes")]
    public async Task<IActionResult> GetDefaultBettypes()
    {
        var betTypes = await soccerDetailService.GetBetTypes(napSession.UserId, BettypeViewPermission.All);

        var betTypeList = from betType in betTypes
            where betType.IsDefaultCheck
            select betType.BetTypeId;

        return Ok(new
        {
            DefaultBettypes = betTypeList
        });
    }
}