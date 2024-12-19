using BetCount.Criteria;
using BetCount.Services;
using Microsoft.AspNetCore.Mvc;

namespace BetCount.Controller;

[Route("api/bet-count")]
[ApiController]
public class BetCountApiController(BetCountService service, BetCountExportExcelService excelService) : ControllerBase
{

    [Route("main")]
    public async Task<IActionResult> GetReport(BetCountCriteria criteria)
    {
        var result = await service.GetReport(criteria);

        return Ok(result);
    }

    [Route("sort")]
    public async Task<IActionResult> SortReport(BetCountCriteria criteria)
    {
        var result = await service.SortReport(criteria);

        return Ok(result);
    }

    [Route("product")]
    public async Task<IActionResult> GetProduct(ProductCriteria criteria)
    {
        var result = await service.GetProduct(criteria);

        return Ok(result);
    }

    [Route("sport")]
    public async Task<IActionResult> GetSport(ProductCriteria criteria)
    {
        var result = await service.GetSport(criteria);

        return Ok(result);
    }

    [HttpPost("export")]
    public async Task<IActionResult> ExportExcel(BetCountCriteria criteria)
    {
        var data = await service.GetReport(criteria);
        var title = $"Bet count report from {criteria.Datestart: MMM dd, yyyy} to {criteria.Dateend: MMM dd, yyyy} ";
        var excelPackage = await excelService.ExportExcel(data, title);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileDownloadName = $"Bet_Count_Report_From_{criteria.Datestart: MM_dd_yyyy}_To_{criteria.Dateend: MM_dd_yyyy}.xlsx";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
    }
}