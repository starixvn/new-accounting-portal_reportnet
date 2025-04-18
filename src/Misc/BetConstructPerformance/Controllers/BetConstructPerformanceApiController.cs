namespace BetConstructPerformance.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Requests;
using Utilities.ExcelResult;

[Route("api/bet-construct")]
[ApiController]
public class BetConstructPerformanceApiController(IMediator mediator) : ControllerBase
{
    [Route("sports")]
    public async Task<IActionResult> Sports(SportRequest request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("excel-sports")]
    [HttpPost]
    public async Task<IActionResult> Excel(SportExcelRequest request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }
}