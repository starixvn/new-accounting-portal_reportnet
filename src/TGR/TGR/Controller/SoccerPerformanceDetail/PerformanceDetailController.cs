using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using TGR.Constants;
using TGR.Criteria.SoccerPerformanceDetail;
using TGR.Services;
using TGR.Services.SoccerPerformanceDetail;
using TGR.Utilities;

namespace TGR.Controller.SoccerPerformanceDetail;

[Route("api/soccer-performance-detail")]
[ApiController]
public class PerformanceDetailController(SoccerPerformanceDetailService soccerDetailService,
    ExcelService excelService,
    BetChoiceExcelService betChoiceExcelService,
    LiveLeagueExcelService liveLeagueService,
    INapSession napSession,
    PermissionService permissionService) : ControllerBase
{
    [Route("bettypes")]
    public async Task<IActionResult> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria)
    {
        var betTypePerformance = await soccerDetailService.GetBetTypePerformance(criteria);

        return Ok(betTypePerformance);
    }

    [Route("default-bettypes")]
    public async Task<IActionResult> GetDefaultBettypes()
    {
        var betTypePermission = await permissionService.GetBettypePermission();
        var betTypes = await soccerDetailService.GetBetTypes(napSession.UserId, betTypePermission);

        var betTypeList = from betType in betTypes
                          where betType.IsDefaultCheck
                          select betType.BetTypeId;

        return Ok(new
        {
            DefaultBettypes = betTypeList
        });
    }

    [HttpPost("export-bettypes")]
    public async Task<IActionResult> ExportExcelBetType([FromForm] GetBetTypePerformanceCriteria criteria)
    {
        var betTypePermission = await permissionService.GetBettypePermission();
        var leagueGroups = await soccerDetailService.GetLeagueGroups();
        var betTypes = await soccerDetailService.GetBetTypes(napSession.UserId, betTypePermission);
        var data = await soccerDetailService.GetBetTypePerformance(criteria);
        var allowVirtualPerformance = permissionService.IsVirtualPerformancePermission();
        var allowRealPerformance = permissionService.IsRealPerformancePermission();

        var excelPackage = await excelService.ExportExcelBetTypePerformance(data, criteria, leagueGroups, betTypes, allowVirtualPerformance, allowRealPerformance);
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

    [HttpPost("export-bet-choice-detail")]
    public async Task<IActionResult> ExportExcelBetChoicePerformanceOfBetType([FromForm] GetBetChoicePerformanceOfBetTypeCriteria criteria)
    {
        var betTypePermission = await permissionService.GetBettypePermission();
        var leagueGroups = await soccerDetailService.GetLeagueGroups();
        var betTypes = await soccerDetailService.GetBetTypes(napSession.UserId, betTypePermission);
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

    [HttpPost("export-live-league-detail")]
    public async Task<IActionResult> ExportLeaguePerformanceOfBetChoice([FromForm] GetLeaguePerformanceOfBetChoiceCriteria criteria)
    {
        var betTypePermission = await permissionService.GetBettypePermission();
        var leagueGroups = await soccerDetailService.GetLeagueGroups();
        var betTypes = await soccerDetailService.GetBetTypes(napSession.UserId, betTypePermission);
        var betTypesString = ModelsHelper.MakeBetTypeSelectString(betTypes, criteria.BetType.ToString());
        var market = criteria.Market switch
        {
            -1 => "All",
            1 => ExcelConstants.LiveText,
            _ => ExcelConstants.NonLiveText
        };
        criteria.IsExcludeLeagues = true;
        var allowVirtualPerformance = permissionService.IsVirtualPerformancePermission();
        var allowRealPerformance = permissionService.IsRealPerformancePermission();

        var data = await soccerDetailService.GetLeaguePerformanceOfBetChoice(criteria);
        var excelPackage = await liveLeagueService.ExportExcel(data, criteria, leagueGroups, betTypesString, allowVirtualPerformance, allowRealPerformance);
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

    [Route("main-market-betchoice")]
    public async Task<IActionResult> GetPointHDPPerformanceOfBetTypeBetChoice(GetPointHDPPerformanceOfBetTypeBetChoiceCriteria criteria)
    {
        var result = await soccerDetailService.GetPointHDPPerformanceOfBetTypeBetChoice(criteria);

        return Ok(result);
    }

    [Route("main-market-betchoice-league")]
    public async Task<IActionResult> GetLeaguePerformanceOfBetTypeBetChoicePointHDP(GetLeagueOfBetTypeBetChoicePointHDPCriteria criteria)
    {

        var result = await soccerDetailService.GetLeaguePerformanceOfBetTypeBetChoicePointHDP(criteria);

        return Ok(result);
    }

    [Route("main-league-betchoice-choice")]
    public async Task<IActionResult> GetPointHDPPerformanceOfBetTypeLeagueBetChoice(GetPointHDPPerformanceOfBetTypeLeagueBetChoiceCriteria criteria)
    {
        var result = await soccerDetailService.GetPointHDPPerformanceOfBetTypeLeagueBetChoice(criteria);

        return Ok(result);
    }

    [HttpPost]
    [Route("GetIntervalPerformanceOfBetTypeBetChoice")]
    public IActionResult GetIntervalPerformanceOfBetTypeBetChoice(GetIntervalPerformanceOfBetTypeBetChoiceCriteria criteria)
    {
        var result = soccerDetailService.GetIntervalPerformanceOfBetTypeBetChoice(criteria);

        return Ok(result);
    }

    [HttpPost]
    [Route("GetTimeMachinePerformanceOfBetTypeBetChoicePointHDP")]
    public IActionResult GetTimeMachinePerformanceOfBetTypeBetChoicePointHDP(GetTimeMachineOfBetTypeBetChoicePointHDPCriteria criteria)
    {
        var result = soccerDetailService.GetTimeMachinePerformanceOfBetTypeBetChoicePointHDP(criteria);

        return Ok(result);
    }

    [HttpPost]
    [Route("GetIntervalPerformanceOfBetTypeLeagueBetChoice")]
    public IActionResult GetIntervalPerformanceOfBetTypeLeagueBetChoice(GetIntervalPerformanceOfBetTypeLeagueBetChoiceCriteria criteria)
    {
        var result = soccerDetailService.GetIntervalPerformanceOfBetTypeLeagueBetChoice(criteria);

        return Ok(result);
    }

    [HttpPost]
    [Route("GetTimeMachinePerformanceOfBetTypeLeagueBetChoicePointHDP")]
    public IActionResult GetTimeMachinePerformanceOfBetTypeLeagueBetChoicePointHDP(GetTimeMachinePerformanceOfBetTypeLeagueBetChoicePointHDPCriteria criteria)
    {
        var result = soccerDetailService.GetTimeMachinePerformanceOfBetTypeLeagueBetChoicePointHDP(criteria);

        return Ok(result);
    }
}