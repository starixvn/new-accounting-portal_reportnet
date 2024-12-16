using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;
using PunterAnalysis.BetTypeGroup.Model;

namespace PunterAnalysis.BetTypeGroup;

[Route("api/punter-analysis/bet-type-group")]
[ApiController]
public class PunterAnalysisBetTypeGroupController(IMediator mediator) : ControllerBase
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