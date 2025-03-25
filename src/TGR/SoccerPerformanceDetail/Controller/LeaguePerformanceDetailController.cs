using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;
using SoccerPerformanceDetail.ViewModel;

namespace SoccerPerformanceDetail.Controller;

[Route("api/soccer-performance-detail")]
[ApiController]
public class LeaguePerformanceDetailController(SoccerPerformanceDetailService service) : ControllerBase
{
    public async Task<IActionResult> GetLeaguePerformance(GetLeaguePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.LeagueName = criteria.LeagueName ?? string.Empty;
        
        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }

        var leaguePerformance = service.GetLeaguePerformance(criteria, criteria.MarketFilter == Market.All);

        return Ok(leaguePerformance);
    }
}
