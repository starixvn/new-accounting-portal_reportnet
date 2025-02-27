using Fanex.NAP.Client;
using SoccerPerformanceDetail.Model;

namespace SoccerPerformanceDetail.Controller;

using Criteria;
using Microsoft.AspNetCore.Mvc;
using Services;
using SoccerPerformanceDetail.ViewModel;
using Utilities.Constants;

[Route("api/soccer-performance-detail")]
[ApiController]
public class PerformanceDetailController(SoccerPerformanceDetailService soccerDetailService,
    SoccerPerformanceDetailExcelService soccerDetailExcelService,
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

    [HttpGet("export-bettypes")]
    public async Task<IActionResult> ExportExcelBetType([FromQuery] GetBetTypePerformanceCriteria criteria)
    {
        var leagueGroups = await soccerDetailService.GetLeagueGroups();
        var betTypes = await soccerDetailService.GetBetTypes(napSession.UserId, BettypeViewPermission.All);
        var data = await soccerDetailService.GetBetTypePerformance(criteria);
        var excelPackage = await soccerDetailExcelService.ExportExcelBetTypePerformance(data, criteria, leagueGroups, betTypes);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_BetType_{criteria.FromDate:yyyyMMdd}_{criteria.FromDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Route("bet-choice-detail")]
    public async Task<IActionResult> GetBetChoicePerformanceOfBetType(GetBetChoicePerformanceOfBetTypeCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;

        var result = await soccerDetailService.GetBetTypeBetChoicePerformance(criteria);

        return Ok(result);
    }

    [Route("trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfBetType(GetTraderPerformanceOfBetTypeCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        TraderPerformanceList traderPerformance = await soccerDetailService.GetTraderPerformanceOfBetType(criteria);
      
        return Ok(traderPerformance);
    }

    [Route("league-detail")]
    public async Task<IActionResult> GetLeaguePerformanceOfBetType(GetLeaguePerformanceOfBetTypeCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var leaguePerformance = await soccerDetailService.GetLeaguePerformanceOfBetType(criteria);

        return Ok(leaguePerformance);
    }

    [Route("live-league-detail")]
    public async Task<IActionResult> GetLeaguePerformanceOfBetChoice(GetLeaguePerformanceOfBetChoiceCriteria criteria)
    {
        criteria.IsExcludeLeagues = true;
        var leaguePerformance = await soccerDetailService.GetLeaguePerformanceOfBetChoice(criteria);

        return Ok(leaguePerformance);
    }
}