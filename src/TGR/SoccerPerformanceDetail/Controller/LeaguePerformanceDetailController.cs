using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;
using SoccerPerformanceDetail.Model;

namespace SoccerPerformanceDetail.Controller;

[Route("api/soccer-performance-detail/league")]
[ApiController]
public class LeaguePerformanceDetailController(SoccerPerformanceDetailService service,
    INapSession napSession,
    LeagueExcelService excelService) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetLeaguePerformance(GetLeaguePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        criteria.LeagueName ??= string.Empty;

        if (criteria.IsAllLeagueGroup)
        {
            criteria.LeagueGroupIds = string.Empty;
        }

        var leaguePerformance = await service.GetLeaguePerformance(criteria, criteria.MarketFilter == Market.All);

        return Ok(leaguePerformance);
    }

    [HttpGet("export-league")]
    public async Task<IActionResult> ExportExcelLeague([FromQuery] GetLeaguePerformanceCriteria criteria)
    {
        var leagueGroups = await service.GetLeagueGroups();
        var betTypes = await service.GetBetTypes(napSession.UserId, BettypeViewPermission.All);
        var data = await service.GetLeaguePerformance(criteria, criteria.MarketFilter == Market.All);
        var excelPackage = await excelService.ExportExcelLeaguePerformance(data, criteria, leagueGroups, betTypes);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_League_{criteria.FromDate:yyyyMMdd}_{criteria.FromDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Route("league-trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfLeague(GetTraderPerformanceOfLeagueCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var traderPerformance = await service.GetTraderPerformanceOfLeague(criteria);

        return Ok(traderPerformance);
    }

    [Route("league-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfLeagueTrader(GetMatchPerformanceOfLeagueTraderCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var matchPerformance = await service.GetMatchPerformanceOfLeagueTrader(criteria);

        return Ok(matchPerformance);
    }

    [Route("league-bettype-detail")]
    public async Task<IActionResult> GetBettypeDetailLeague(GetBettypeDetailLeague criteria)
    {
        criteria.IsExcludeLeagues = true;
       
        var bettypeDetail = await service.GetBettypeDetailLeague(criteria);
        
        return Ok(bettypeDetail);
    }

    [Route("match-performance")]
    public async Task<IActionResult> GetMatchPerformanceOfLeague(GetMatchPerformanceOfLeagueCriteria criteria)
    {
        criteria.IsExcludeLeagues =true;
        var matchPerformance = await service.GetMatchPerformanceOfLeague(criteria);
        return Ok(matchPerformance);
    }

    [Route("match-bettype-performance")]
    public async Task<IActionResult> GetBettypePerformanceOfLeagueMatch(GetBetTypePerformanceOfLeagueMatchCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var bettypePerformance = await service.GetBettypePerformanceOfLeagueMatch(criteria);

        return Ok(bettypePerformance);
    }
}
