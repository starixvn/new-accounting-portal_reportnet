using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria.JuiceLeftOver;
using TGR.Services.JuiceLeftOver;

namespace TGR.Controller.JuiceLeftOver;

[Route("api/juice-left-over/team")]
[ApiController]
public class TeamController(TeamService service, ExcelService excelService) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetTeamPerformance(GetTeamPerformanceCriteria criteria)
    {
        var result = await service.GetTeamPerformance(criteria);

        return Ok(result);
    }

    [Route("team-trader")]
    public async Task<IActionResult> GetTraderPerformanceOfTeam(GetTraderPerformanceOfTeamCriteria criteria)
    {
        var result = await service.GetTeamTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("team-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfTeamTrader(GetMatchPerformanceOfTeamTraderCriteria criteria)
    {
        var result = await service.GetTeamTraderMatchPerformance(criteria);

        return Ok(result);
    }

    [HttpPost("export-teams")]
    public async Task<IActionResult> ExportTeamPerformance([FromForm]GetTeamPerformanceCriteria criteria)
    {
        var data = await service.GetTeamPerformance(criteria);
        var teams = await service.GetTeams();
        var excelPackage = await excelService.ExportExcelTeamPerformance(data, criteria, teams);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_Team_(Juice-LeftOver)_{criteria.FromDate:yyyyMMdd}_{criteria.FromDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Route("team-league-trader-match-bettype")]
    public async Task<IActionResult> GetBettypeDetailTeam(GetBettypeDetailTeamCriteria criteria)
    {
        var result = await service.GetTeamLeagueTraderMatchBettypePerformance(criteria);

        return Ok(result);
    }

    [Route("team-league")]
    public async Task<IActionResult> GetLeagueDetailTeam([FromForm]GetLeagueDetailTeamCriteria criteria)
    {
        var result = await service.GetTeamLeaguePerformance(criteria);

        return Ok(result);
    }

    [Route("team-league-trader")]
    public async Task<IActionResult> GetTraderPerformanceOfLeagueTeam(GetTraderPerformanceOfLeagueTeam criteria)
    {
        var result = await service.GetTeamLeagueTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("team-league-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfTraderLeagueTeam(GetMatchPerformanceOfTraderLeagueTeamCriteria criteria)
    {
        var result = await service.GetTeamLeagueTraderMatchPerformance(criteria);

        return Ok(result);
    }
}
