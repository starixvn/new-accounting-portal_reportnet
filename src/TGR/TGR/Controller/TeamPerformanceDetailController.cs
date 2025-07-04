using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria;
using TGR.Services;

namespace TGR.Controller;


[Route("api/soccer-performance-detail/team")]
[ApiController]
public class TeamPerformanceDetailController(SoccerPerformanceDetailService service,
    INapSession napSession,
    TeamExcelService excelService,
    PermissionService permissionService) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetTeamPerformance(GetTeamPerformanceCriteria criteria)
    {
        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }
        
        var teamPerformance = await service.GetTeamPerformance(criteria);

        return Ok(teamPerformance);
    }

    [HttpGet("export-team")]
    public async Task<IActionResult> ExportExcelTeam([FromQuery] GetTeamPerformanceCriteria criteria)
    {
        var betTypePermission = await permissionService.GetBettypePermission();
        var leagueGroups = await service.GetLeagueGroups();
        var betTypes = await service.GetBetTypes(napSession.UserId, betTypePermission);
        var data = await service.GetTeamPerformance(criteria);
        var allowVirtualPerformance = permissionService.IsVirtualPerformancePermission();
        var allowRealPerformance = permissionService.IsRealPerformancePermission();

        var excelPackage = await excelService.ExportExcelTeamPerformance(data, criteria, leagueGroups, betTypes, allowVirtualPerformance, allowRealPerformance);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_Team_{criteria.FromDate:yyyyMMdd}_{criteria.FromDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Route("team-trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfTeam(GetTraderPerformanceOfTeamCriteria criteria)
    {
        var traderPerformance = await service.GetTraderPerformanceOfTeam(criteria);
        
        return Ok(traderPerformance);
    }

    [Route("team-league-detail")]
    public async Task<IActionResult> GetLeagueDetailTeam(GetLeagueDetailTeamCriteria criteria)
    {
        var teamPerformanceLeague = await service.GetLeaguePerformance(criteria);

        return Ok(teamPerformanceLeague);
    }

    [Route("team-trader-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfTeamTrader(GetMatchPerformanceOfTeamTraderCriteria criteria)
    {
        var matchPerformance = await service.GetMatchPerformanceOfTeamTrader(criteria);
        
        return Ok(matchPerformance);
    }

    [Route("team-trader-match-bettype-detail")]
    public async Task<IActionResult> GetBettypeDetailTeam(GetBettypeDetailTeamCriteria criteria)
    {
        var bettypeDetail = await service.GetBettypeDetailTeam(criteria);
        
        return Ok(bettypeDetail);
    }

    [Route("team-league-trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfLeagueTeam(GetTraderPerformanceOfLeagueTeam criteria)
    {
        var result = await service.GetTraderPerformance(criteria);
        
        return Ok(result);
    }

    [Route("team-league-trader-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfTraderLeagueTeam(GetMatchPerformanceOfTraderLeagueTeam criteria)
    {
        var data = await service.GetMatchPerformanceOfTrader(criteria);

        return Ok(data);
    }
}
