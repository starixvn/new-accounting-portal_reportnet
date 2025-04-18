namespace DailyCreatedCustomers.Controller;

using Criteria;
using Microsoft.AspNetCore.Mvc;
using Services;
using Utilities.DateTimes;

[Route("api/daily-created-customer")]
[ApiController]
public class DailyCreatedCustomersApiController(DailyCreatedCustomersService service,
    DailyCreatedCustomersExcelService excelService) : ControllerBase
{
    [Route("summary")]
    public async Task<IActionResult> GetSummaryReport(DailyCreatedCustomersCriteria criteria)
    {
        var result = await service.GetReport(criteria);

        return Ok(result);
    }

    [Route("detail")]
    public async Task<IActionResult> GetDetailReport(DailyCreatedCustomersDetailsCriteria criteria)
    {
        var result = await service.GetDetailReport(criteria);

        return Ok(result);
    }

    [HttpGet("export-summary")]
    public async Task<IActionResult> ExportExcelSummary([FromQuery] DailyCreatedCustomersCriteria criteria)
    {
        var data = await service.GetReport(criteria);
        var title =
            $"Daily Created Customers Report from {criteria.Fromdate: MMM dd, yyyy} to {criteria.Todate: MMM dd, yyyy}";
        var excelPackage = await excelService.ExportExcel(data, title);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileDownloadName = $"Daily_Created_Customer_Report_From_{criteria.Fromdate: MM_dd_yyyy}_To_{criteria.Todate: MM_dd_yyyy}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileDownloadName}.xlsx");
    }

    [HttpGet("export-detail")]
    public async Task<IActionResult> ExportExcelDetail([FromQuery] DailyCreatedCustomersDetailsCriteria criteria)
    {
        var data = await service.GetDetailReport(criteria);
        var title = $"{criteria.RoleName} List ({criteria.Date.ToShortFormatedDate()})";
        var excelPackage = await excelService.ExportExcelDetail(data, title);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileDownloadName = $"{criteria.RoleName}_List_Created_On_{criteria.Date.ToShortFormatedDateWithSlash().Replace("/", "_")}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileDownloadName}.xlsx");
    }
}