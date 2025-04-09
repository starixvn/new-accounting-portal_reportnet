namespace SoccerPerformanceDetail.Controller;

using Constants;
using Criteria;
using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using Utilities;

[Route("api/soccer-performance-detail")]
[ApiController]
public class PerformanceDetailController(SoccerPerformanceDetailService soccerDetailService,
    ExcelService excelService,
    BetChoiceExcelService betChoiceExcelService,
    LiveLeagueExcelService liveLeagueService,
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
        var excelPackage = await excelService.ExportExcelBetTypePerformance(data, criteria, leagueGroups, betTypes);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_BetType_{criteria.FromDate:yyyyMMdd}_{criteria.FromDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Route("bet-choice-detail")]
    public async Task<IActionResult> GetBetChoicePerformanceOfBetType(GetBetChoicePerformanceOfBetTypeCriteria criteria)
    {
        var result = await soccerDetailService.GetBetTypeBetChoicePerformance(criteria);

        return Ok(result);
    }

    [HttpGet("export-bet-choice-detail")]
    public async Task<IActionResult> ExportExcelBetChoicePerformanceOfBetType([FromQuery] GetBetChoicePerformanceOfBetTypeCriteria criteria)
    {
        var leagueGroups = await soccerDetailService.GetLeagueGroups();
        var betTypes = await soccerDetailService.GetBetTypes(napSession.UserId, BettypeViewPermission.All);
        var betTypesString = ModelsHelper.MakeBetTypeSelectString(betTypes, criteria.BetType.ToString());
        var market = criteria.Market switch
        {
            -1 => "All",
            1 => ExcelConstants.LiveText,
            _ => ExcelConstants.NonLiveText
        };
        criteria.IsExcludeLeagues = true;
        var data = await soccerDetailService.GetBetTypeBetChoicePerformance(criteria);
        var excelPackage = await betChoiceExcelService.ExportExcelBetTypePerformance(data, criteria, leagueGroups, betTypesString);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_{betTypesString.Replace(" ", "_")}_{market}_{criteria.FromDate:yyyyMMdd}_{criteria.ToDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Route("trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfBetType(GetTraderPerformanceOfBetTypeCriteria criteria)
    {
        var traderPerformance = await soccerDetailService.GetTraderPerformanceOfBetType(criteria);
      
        return Ok(traderPerformance);
    }

    [Route("league-detail")]
    public async Task<IActionResult> GetLeaguePerformanceOfBetType(GetLeaguePerformanceOfBetTypeCriteria criteria)
    {
        var leaguePerformance = await soccerDetailService.GetLeaguePerformanceOfBetType(criteria);

        return Ok(leaguePerformance);
    }

    [Route("live-league-detail")]
    public async Task<IActionResult> GetLeaguePerformanceOfBetChoice(GetLeaguePerformanceOfBetChoiceCriteria criteria)
    {
        var leaguePerformance = await soccerDetailService.GetLeaguePerformanceOfBetChoice(criteria);

        return Ok(leaguePerformance);
    }

    [HttpGet("export-live-league-detail")]
    public async Task<IActionResult> ExportLeaguePerformanceOfBetChoice([FromQuery]GetLeaguePerformanceOfBetChoiceCriteria criteria)
    {
        var leagueGroups = await soccerDetailService.GetLeagueGroups();
        var betTypes = await soccerDetailService.GetBetTypes(napSession.UserId, BettypeViewPermission.All);
        var betTypesString = ModelsHelper.MakeBetTypeSelectString(betTypes, criteria.BetType.ToString());
        var market = criteria.Market switch
        {
            -1 => "All",
            1 => ExcelConstants.LiveText,
            _ => ExcelConstants.NonLiveText
        };
        criteria.IsExcludeLeagues = true;
        var data = await soccerDetailService.GetLeaguePerformanceOfBetChoice(criteria);
        var excelPackage = await liveLeagueService.ExportExcel(data, criteria, leagueGroups, betTypesString);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName =
            $"Performance_Detail_{betTypesString.Replace(" ", "_")}_{market}_{criteria.BetChoice}_{criteria.FromDate:yyyyMMdd}_{criteria.ToDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Route("trader-match-detail")]
    public async Task<IActionResult> GetMatchPerformanceOfBetTypeTrader(GetMatchPerformanceOfBetTypeTraderCriteria criteria)
    {
        var matchPerformance = await soccerDetailService.GetMatchPerformanceOfBetTypeTrader(criteria);
       
        return Ok(matchPerformance);
    }

    [Route("league-trader-detail")]
    public async Task<IActionResult> GetTraderPerformanceOfBetTypeLeague(GetTraderPerformanceOfBetTypeLeagueCriteria criteria)
    {
        var traderPerformance = await soccerDetailService.GetTraderPerformanceOfBetTypeLeague(criteria);

        return Ok(traderPerformance);
    }

    [Route("league-bet-choice-detail")]
    public async Task<IActionResult> GetBetChoicePerformanceOfBetTypeLeague(GetBetChoicePerformanceOfBetTypeLeagueCriteria criteria)
    {
        var result = await soccerDetailService.GetBetChoicePerformanceOfBetTypeLeague(criteria);

        return Ok(result);
    }

    [Route("league-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfBetTypeLeagueTrader(
        GetMatchPerformanceOfBetTypeLeagueTraderCriteria criteria)
    {
        var result = await soccerDetailService.GetMatchPerformanceOfBetTypeLeagueTrader(criteria);

        return Ok(result);
    }
}