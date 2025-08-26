using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TGR.Criteria.JuiceLeftOver;
using TGR.Services.JuiceLeftOver;

namespace TGR.Controller.JuiceLeftOver;

[Route("api/juice-left-over/league")]
[ApiController]
public class LeagueController(LeagueService service, ExcelService excelService) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetLeaguePerformance(GetLeaguePerformanceCriteria criteria)
    {
        var result = await service.GetLeaguePerformance(criteria);

        return Ok(result);
    }

    [HttpPost("export-leagues")]
    public async Task<IActionResult> ExportExcelLeague([FromForm] GetLeaguePerformanceCriteria criteria)
    {
        var data = await service.GetLeaguePerformance(criteria);

        var excelPackage = await excelService.ExportExcelLeaguePerformance(data, criteria);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_League_(Juice-LeftOver)_{criteria.FromDate:yyyyMMdd}_{criteria.ToDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [HttpPost("export-match")]
    public async Task<IActionResult> ExportExcelMatch([FromForm] GetMatchPerformanceOfLeagueNameCriteria criteria)
    {
        var csvData = await service.GetCsvDataForMatchPerformance(criteria);
        var csvFileName =
            $"Performance_Detail_All_Matches_(Juice-LeftOver)_{criteria.FromDate:yyyyMMdd}_{criteria.ToDate:yyyyMMdd}.csv";
        var encoded = Encoding.UTF8.GetBytes(csvData);

        return File(encoded, "text/plain", csvFileName);
    }

    [Route("league-trader")]
    public async Task<IActionResult> GetTraderPerformanceOfLeague(GetTraderPerformanceOfLeagueCriteria criteria)
    {
        var result = await service.GetTraderPerformanceByLeague(criteria);

        return Ok(result);
    }

    [Route("league-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfLeagueTrader(GetMatchPerformanceOfLeagueTraderCriteria criteria)
    {
        var result = await service.GetMatchPerformanceOfLeagueAndTrader(criteria);

        return Ok(result);
    }

    [Route("league-bettype")]
    public async Task<IActionResult> GetBettypeDetailLeague(GetBettypeDetailLeagueCriteria criteria)
    {
        var result = await service.GetBettypePerformanceOfLeagueTraderAndMatch(criteria);

        return Ok(result);
    }

    [Route("league-match")]
    public async Task<IActionResult> GetMatchPerformanceOfLeague(GetMatchPerformanceOfLeagueCriteria criteria)
    {
        var result = await service.GetMatchPerformanceOfLeague(criteria);

        return Ok(result);
    }

    [Route("league-match-bettype")]
    public async Task<IActionResult> GetBettypePerformanceOfLeagueMatch(GetBetTypePerformanceOfLeagueMatchCriteria criteria)
    {
        var result = await service.GetBettypePerformanceOfLeagueAndMatch(criteria);

        return Ok(result);
    }
}
