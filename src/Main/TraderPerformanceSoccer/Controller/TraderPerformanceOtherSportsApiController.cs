namespace TraderPerformance.Controller;

using Criteria;
using Microsoft.AspNetCore.Mvc;
using Services;

[Route("api/trader-performance-other-sports")]
[ApiController]
public class TraderPerformanceOtherSportsApiController(TraderPerformanceOtherSportsService service,
    TraderPerformanceOtherSportsExcelService excelService) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult> GetTraderPerformanceSoccer(ReportCriteria criteria)
    {
        var result = await service.GetReport(criteria);

        return Ok(result);
    }

    [Route("detail")]
    public async Task<IActionResult> GetTraderPerformanceSoccerDetail(DetailReportCriteria criteria)
    {
        var result = await service.GetDetailReport(criteria);

        return Ok(result);
    }

    [Route("main-sort")]
    public async Task<IActionResult> SortTraderPerformanceSoccer(ReportCriteria criteria)
    {
        var result = await service.SortReportData(criteria);

        return Ok(result);
    }

    [Route("detail-sort")]
    public async Task<IActionResult> SortTraderPerformanceSoccerDetail(DetailReportCriteria criteria)
    {
        var result = await service.SortDetailReportData(criteria);

        return Ok(result);
    }

    [HttpGet("export-main")]
    public async Task<IActionResult> ExportExcelTraderPerformanceSoccer([FromQuery] ReportCriteria criteria)
    {
        var data = await service.GetReport(criteria);
        var sportText = criteria.IsSoccer ? "SOCCER SPORT" : "OTHER SPORTS";
        var title =
            $"Trader Performance For {sportText} - From Date: {criteria.Fromdate: MMM dd, yyyy} To Date: {criteria.Todate: MMM dd, yyyy}";
        var excelPackage = await excelService.ExportExcelOtherSports(data, title);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var sportTextFileName = criteria.IsSoccer ? "Soccer" : "OtherSports";
        var fileDownloadName = $"TraderPerformance_{sportTextFileName}_From_Date_{criteria.Fromdate: MM_dd_yyyy}_To_Date_{criteria.Todate: MM_dd_yyyy}.xlsx";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
    }

    [HttpGet("export-detail")]
    public async Task<IActionResult> ExportExcelTraderPerformanceSoccerDetail([FromQuery] DetailReportCriteria criteria)
    {
        var data = await service.GetDetailReport(criteria);
        var sportText = criteria.IsSoccer ? "SOCCER SPORT Detail" : "OTHER SPORTS Detail";
        var title =
            $"Trader Performance For {sportText} - From Date: {criteria.Fromdate: MMM dd, yyyy} To Date: {criteria.Todate: MMM dd, yyyy}";
        var excelPackage = await excelService.ExportExcelOtherSportsDetail(data, title);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var sportTextFileName = criteria.IsSoccer ? "Soccer_Detail" : "OtherSports_Detail";
        var fileDownloadName = $"TraderPerformance_{sportTextFileName}_From_Date_{criteria.Fromdate: MM_dd_yyyy}_To_Date_{criteria.Todate: MM_dd_yyyy}.xlsx";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
    }
}