using MediatR;
using Microsoft.AspNetCore.Mvc;

using PunterAnalysis.SportType.Model;

namespace PunterAnalysis.SportType;

using Utilities.ExcelResult;

[Route("api/punter-analysis/sport-type")]
[ApiController]
public class PunterAnalysisSportTypeController(IMediator mediator) : ControllerBase
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