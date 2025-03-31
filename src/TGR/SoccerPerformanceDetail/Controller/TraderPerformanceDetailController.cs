using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Model;
using SoccerPerformanceDetail.Services;

namespace SoccerPerformanceDetail.Controller;

[Route("api/soccer-performance-detail/trader")]
[ApiController]
public class TraderPerformanceDetailController(SoccerPerformanceDetailService service,
    INapSession napSession,
    TraderExcelService excelService) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetTraderPerformance(GetTraderPerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
            
        criteria.Username ??= string.Empty;
            
        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }
           
        var traderPerformance = await service.GetTraderPerformance(criteria);
            
        return Ok(traderPerformance);
    }

    [HttpGet("export-trader")]
    public async Task<IActionResult> ExportExcelLeague([FromQuery] GetTraderPerformanceCriteria criteria)
    {
        var leagueGroups = await service.GetLeagueGroups();
        var betTypes = await service.GetBetTypes(napSession.UserId, BettypeViewPermission.All);
        var data = await service.GetTraderPerformance(criteria);
        var excelPackage = await excelService.ExportExcelTraderPerformance(data, criteria, leagueGroups, betTypes);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_Trader_{criteria.FromDate:yyyyMMdd}_{criteria.FromDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Route("trader-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfTrader(GetMatchPerformanceOfTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var matchPerformanceOfTrader = await service.GetMatchPerformanceOfTrader(criteria);
           
        return Ok(matchPerformanceOfTrader);
    }

    [Route("trader-bettype-detail")]
    public async Task<IActionResult> GetBettypeDetailTrader(GetBettypeDetailTrader criteria)
    {
        criteria.IsExcludeLeagues = true;
        var bettypeDetail = await service.GetBettypeDetailTrader(criteria);
            
        return Ok(bettypeDetail);
    }
}