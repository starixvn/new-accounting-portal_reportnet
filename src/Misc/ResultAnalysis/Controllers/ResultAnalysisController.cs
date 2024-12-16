using MediatR;
using Microsoft.AspNetCore.Mvc;

using ResultAnalysis.Requests;

namespace ResultAnalysis.Controllers;

using Utilities.ExcelResult;

[Route("api/result-analysis")]
[ApiController]
public class ResultAnalysisController(IMediator mediator) : ControllerBase
{
    [Route("leagues")]
    public async Task<IActionResult> Leagues(LeagueRequest request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("result-analysis")]
    public async Task<IActionResult> ResultAnalysis(ResultAnalysisRequest request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("result-analysis-of-quarter")]
    public async Task<IActionResult> ResultAnalysisOfQuarter(ResultAnalysisOfQuarterRequest request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("export-excel")]
    [HttpPost]
    public async Task<IActionResult> Excel(ResultAnalysisExcelRequest request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }
}