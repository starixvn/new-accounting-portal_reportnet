using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria;
using TGR.Services;

namespace TGR.Controller;

[Route("api/soccer-performance-detail/league")]
[ApiController]
public class LeaguePerformanceDetailController(SoccerPerformanceDetailService service,
    INapSession napSession,
    LeagueExcelService excelService,
    PermissionService permissionService) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetLeaguePerformance(GetLeaguePerformanceCriteria criteria)
    {
        criteria.LeagueName ??= string.Empty;

        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }

        var leaguePerformance = await service.GetLeaguePerformance(criteria, criteria.MarketFilter == Market.All);

        return Ok(leaguePerformance);
    }

    [HttpPost("export-league")]
    public async Task<IActionResult> ExportExcelLeague([FromBody] GetLeaguePerformanceCriteria criteria)
    {
        var betTypePermission = await permissionService.GetBettypePermission();
        var leagueGroups = await service.GetLeagueGroups();
        var betTypes = await service.GetBetTypes(napSession.UserId, betTypePermission);
        var data = await service.GetLeaguePerformance(criteria, criteria.MarketFilter == Market.All);
        var allowVirtualPerformance = permissionService.IsVirtualPerformancePermission();
        var allowRealPerformance = permissionService.IsRealPerformancePermission();

        var excelPackage = await excelService.ExportExcelLeaguePerformance(data, criteria, leagueGroups, betTypes, allowVirtualPerformance, allowRealPerformance);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_League_{criteria.FromDate:yyyyMMdd}_{criteria.FromDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Route("league-trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfLeague(GetTraderPerformanceOfLeagueCriteria criteria)
    {
        var traderPerformance = await service.GetTraderPerformanceOfLeague(criteria);

        return Ok(traderPerformance);
    }

    [Route("league-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfLeagueTrader(GetMatchPerformanceOfLeagueTraderCriteria criteria)
    {
        var matchPerformance = await service.GetMatchPerformanceOfLeagueTrader(criteria);

        return Ok(matchPerformance);
    }

    [Route("league-bettype-detail")]
    public async Task<IActionResult> GetBettypeDetailLeague(GetBettypeDetailLeague criteria)
    {
        var bettypeDetail = await service.GetBettypeDetailLeague(criteria);
        
        return Ok(bettypeDetail);
    }

    [Route("match-performance")]
    public async Task<IActionResult> GetMatchPerformanceOfLeague(GetMatchPerformanceOfLeagueCriteria criteria)
    {
        var matchPerformance = await service.GetMatchPerformanceOfLeague(criteria);
        return Ok(matchPerformance);
    }

    [Route("match-bettype-performance")]
    public async Task<IActionResult> GetBettypePerformanceOfLeagueMatch(GetBetTypePerformanceOfLeagueMatchCriteria criteria)
    {
        var bettypePerformance = await service.GetBettypePerformanceOfLeagueMatch(criteria);

        return Ok(bettypePerformance);
    }
}
