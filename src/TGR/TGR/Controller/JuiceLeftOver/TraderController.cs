using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria.JuiceLeftOver;
using TGR.Services.JuiceLeftOver;

namespace TGR.Controller.JuiceLeftOver;

[Route("api/juice-left-over/trader")]
[ApiController]
public class TraderController(TraderService service, ExcelService excelService) : ControllerBase
{
    [Route("main")]

    public async Task<IActionResult>  GetTraderPerformance(GetTraderPerformanceCriteria criteria)
    {
        var traderPerformance = await service.GetTraderPerformance(criteria);

        return Ok(traderPerformance);
    }

    [Route("trader-match")]
    public async Task<IActionResult>  GetTraderMatchPerformance(GetTraderMatchPerformanceCriteria criteria)
    {
        var traderMatchPerformance = await service.GetTraderMatchPerformance(criteria);

        return Ok(traderMatchPerformance.GroupBy(l => l.LeagueId));
    }

    [Route("trader-match-bettype")]
    public async Task<IActionResult>  GetTraderMatchBettypePerformance(GetTraderMatchBettypePerformanceCriteria criteria)
    {
        var traderMatchBettypePerformance = await service.GetTraderMatchBettypePerformance(criteria);

        return Ok(traderMatchBettypePerformance);
    }

    [HttpPost("export-trader")]
    public async Task<IActionResult> ExportTraderPerformance([FromForm]GetTraderPerformanceCriteria criteria)
    {
        var data = await service.GetTraderPerformance(criteria);
        var excelPackage = await excelService.ExportExcelTraderPerformance(data, criteria);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_Trader_(Juice-LeftOver)_{criteria.FromDate:yyyyMMdd}_{criteria.ToDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }
}
