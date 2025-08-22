using Fanex.NAP.Client;
using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria.JuiceLeftOver;
using TGR.Services;
using TGR.Services.JuiceLeftOver;

namespace TGR.Controller.JuiceLeftOver;

[Route("api/juice-left-over/bettype")]
[ApiController]
public class BetTypeController(BetTypeService service, ExcelService excelService) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria)
    {
        var result = await service.GetBetTypePerformance(criteria);

        return Ok(result);
    }

    [Route("bettype-trader")]
    public async Task<IActionResult> GetTraderPerformanceOfBetType(GetTraderPerformanceOfBetTypeCriteria criteria)
    {
        var result = await service.GetBetTypeTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("bettype-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfBetTypeTrader(GetMatchPerformanceOfBetTypeTraderCriteria criteria)
    {
        var result = await service.GetBetTypeTraderMatchPerformance(criteria);

        return Ok(result);
    }

    [Route("bettype-league")]
    public async Task<IActionResult> GetLeaguePerformanceOfBetType(GetLeaguePerformanceOfBetTypeCriteria criteria)
    {
        var result = await service.GetBetTypeLeaguePerformance(criteria);

        return Ok(result);
    }

    [Route("bettype-league-trader")]
    public async Task<IActionResult> GetTraderPerformanceOfBetTypeLeague(GetTraderPerformanceOfBetTypeLeagueCriteria criteria)
    {
        var result = await service.GetBetTypeLeagueTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("bettype-league-trader-match")]
    public async Task<IActionResult> GetMatchPerformanceOfBetTypeLeagueTrader(GetMatchPerformanceOfBetTypeLeagueTraderCriteria criteria)
    {
        var result = await service.GetBetTypeLeagueTraderMatchPerformance(criteria);

        return Ok(result);
    }

    [HttpPost("export-bettypes")]
    public async Task<IActionResult> ExportExcelBetType([FromForm] GetBetTypePerformanceCriteria criteria)
    {
        var data = await service.GetBetTypePerformance(criteria);

        var excelPackage = await excelService.ExportExcelBetTypePerformance(data, criteria);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_BetType_(Juice-LeftOver)_{criteria.FromDate:yyyyMMdd}_{criteria.ToDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }
}
