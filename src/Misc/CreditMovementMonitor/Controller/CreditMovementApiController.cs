using CreditMovementMonitor.Criteria;
using CreditMovementMonitor.Services;
using Fanex.Excel;
using Microsoft.AspNetCore.Mvc;
using Utilities.DateTimes;

namespace CreditMovementMonitor.Controller;

[Route("api/credit-movement")]
[ApiController]
public class CreditMovementApiController(CreditMovementService service) : ControllerBase
{
    [Route("sites")]
    public async Task<IActionResult> GetSites()
    {
        var result = await service.GetSites();

        return Ok(result);
    }

    [Route("report")]
    public async Task<IActionResult> GetReport(CreditMovementCriteria criteria)
    {
        var result = await service.GetReport(criteria);

        return Ok(result);
    }

    [HttpGet]
    [Route("export")]
    public async Task<IActionResult> ExportExcel([FromQuery] CreditMovementCriteria criteria)
    {
        var gridOptions = await service.GetFullReportFromCache(criteria);
        var excel = new FanexExcel(gridOptions);
        var workBook = excel.ToExcel2003("Credit Movement");
        var memoryStream = new MemoryStream();
        workBook.Write(memoryStream);
        var fromDate = criteria.FromDate.ToShortFormatedDateWithSlash().Replace('/', '_');
        var toDate = criteria.ToDate.ToShortFormatedDateWithSlash().Replace('/', '_');
        var fileName =
            $"Credit_Movement_Report_From_{fromDate}_To_{toDate}.xls";
        return File(memoryStream.ToArray(), "application/vnd.ms-excel", fileName);
    }
}