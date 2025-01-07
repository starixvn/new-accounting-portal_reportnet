using MediatR;
using Microsoft.AspNetCore.Mvc;

using PunterAnalysis.GameBetType.Model;

namespace PunterAnalysis.GameBetType;

using Utilities.ExcelResult;

[Route("api/punter-analysis/game-bet-type")]
[ApiController]
public class PunterAnalysisGameBetTypeController(IMediator mediator) : ControllerBase
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