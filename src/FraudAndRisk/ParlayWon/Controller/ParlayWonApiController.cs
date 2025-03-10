namespace ParlayWon.Controller;

using Criteria;
using Microsoft.AspNetCore.Mvc;
using Services;

[Route("api/parlay-won")]

[ApiController]
public class ParlayWonApiController(ParlayWonService service, ParlayWonExportExcelService excelService) : ControllerBase
{
    [Route("report")]
    public async Task<IActionResult> GetReport(ParlayWonReportCriteria criteria)
    {
        var result = await service.GetReport(criteria);
        return Ok(result);
    }

    [Route("sort")]
    public async Task<IActionResult> SortReport(ParlayWonReportCriteria criteria)
    {
        var result = await service.SortReport(criteria);
        return Ok(result);
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportExcel([FromQuery] ParlayWonReportCriteria criteria)
    {
        var data = await service.GetReport(criteria);
        var title = $"{criteria.Bettype} - {criteria.SportName} Won on {criteria.WinlostDate:MMM dd, yyyy}";
        var excelPackage = await excelService.ExportExcel(data, title);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileDownloadName = $"Parlay_Won_{criteria.WinlostDate: MM_dd_yyyy}.xlsx";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
    }
}