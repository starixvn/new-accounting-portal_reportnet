using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;
using PunterAnalysis.BetCount.Model;

namespace PunterAnalysis.BetCount;

[Route("api/punter-analysis/bet-count")]
[ApiController]
public class PunterAnalysisBetCountController(IMediator mediator) : ControllerBase
{
    [Route("")]
    public async Task<IActionResult> Index(HttpParameterJson parameter)
    {
        var viewModel = await mediator.Send(parameter);
        return Ok(viewModel);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(HttpParameterExcel parameter)
    {
        var excelPackage = await mediator.Send(parameter);
        return new EpplusExcelResult(excelPackage);
    }
}