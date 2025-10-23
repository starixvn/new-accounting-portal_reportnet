using DstPerformance.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

namespace DstPerformance.Controllers;

[Route("api/dst")]
[ApiController]
public class BetConstructPerformanceApiController(IMediator mediator) : ControllerBase
{
    [Route("data")]
    public async Task<IActionResult> Sports(ReportRequest request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("excel-data")]
    [HttpPost]
    public async Task<IActionResult> Excel(ReportExcelRequest request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }
}