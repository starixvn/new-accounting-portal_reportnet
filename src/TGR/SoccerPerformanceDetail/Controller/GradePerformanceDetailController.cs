using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;

namespace SoccerPerformanceDetail.Controller;


[Route("api/soccer-performance-detail/grade")]
[ApiController]
public class GradePerformanceDetailController(SoccerPerformanceDetailService service,
    INapSession napSession,
    GradeExcelService excelService,
    PermissionService permissionService) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetGradePerformance(GetGradePerformanceCriteria criteria)
    {
        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }

        var gradePerformance = await service.GetGradePerformance(criteria);
        
        return Ok(gradePerformance);
    }

    [HttpGet("export-grade")]
    public async Task<IActionResult> ExportExcelTrader([FromQuery] GetGradePerformanceCriteria criteria)
    {
        var betTypePermission = await permissionService.GetBettypePermission();
        var leagueGroups = await service.GetLeagueGroups();
        var betTypes = await service.GetBetTypes(napSession.UserId, betTypePermission);
        var data = await service.GetGradePerformance(criteria);
        var allowVirtualPerformance = permissionService.IsVirtualPerformancePermission();
        var allowRealPerformance = permissionService.IsRealPerformancePermission();

        var excelPackage = await excelService.ExportExcelGradePerformance(data, criteria, leagueGroups, betTypes, allowVirtualPerformance, allowRealPerformance);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_Grade_{criteria.FromDate:yyyyMMdd}_{criteria.FromDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Route("trader-trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfGrade(GetTraderPerformanceOfGradeCriteria criteria)
    {
        var traderPerformance = await service.GetTraderPerformanceOfGrade(criteria);

        return Ok(traderPerformance);
    }

    [Route("trader-league-detail")]
    public async Task<IActionResult> GetLeaguePerformanceOfGrade(GetLeaguePerformanceOfGradeCriteria criteria)
    {
        var leaguePerformance = await service.GetLeaguePerformanceOfGrade(criteria);

        return Ok(leaguePerformance);
    }

    [Route("trader-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfGradeTrader(GetMatchPerformanceOfGradeTraderCriteria criteria)
    {
        var matchPerformance = await service.GetMatchPerformanceOfGradeTrader(criteria);

        return Ok(matchPerformance);
    }

    [Route("trader-bettype-detail")]
    public async Task<IActionResult> GetBettypeDetailGrade(GetBettypeDetailGradeCriteria criteria)
    {
        var bettypeDetail = await service.GetBettypeDetailGrade(criteria);

        return Ok(bettypeDetail);
    }

    [Route("trader-by-league-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfGradeByLeague(GetTraderPerformanceOfGradeByLeagueCriteria criteria)
    {
        var leaguePerformance = await service.GetTraderPerformanceOfGradeByLeague(criteria);

        return Ok(leaguePerformance);
    }

    [Route("grade-league-trader")]
    public async Task<IActionResult> GetMatchPerformanceOfGradeLeagueTrader(GetMatchPerformanceOfLeagueTraderCriteria criteria)
    {
        var matchPerformance = await service.GetMatchPerformanceOfLeagueTrader(criteria);
        return Ok(matchPerformance);
    }
}
