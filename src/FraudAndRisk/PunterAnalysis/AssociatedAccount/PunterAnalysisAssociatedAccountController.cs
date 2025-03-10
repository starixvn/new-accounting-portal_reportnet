namespace PunterAnalysis.AssociatedAccount;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Model;
using OfficeOpenXml;
using Utilities.ExcelResult;

[Route("api/punter-analysis/associated-account")]
[ApiController]
public class PunterAnalysisAssociatedAccountController(IMediator mediator) : ControllerBase
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
        var excelPackage = await mediator.Send<ExcelPackage>(parameter);
        return new EpplusExcelResult(excelPackage);
    }
}