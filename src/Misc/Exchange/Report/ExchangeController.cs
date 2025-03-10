namespace Exchange.Report;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/exchange")]
[ApiController]
public class ExchangeController(IMediator mediator) : ControllerBase
{
    [Route("main-report")]
    public async Task<IActionResult> ExchangeLincensee(LicenseeHttpParameterJson httpParameter)
    {
        var viewModel = await mediator.Send(httpParameter);
        return Ok(viewModel);
    }

    [Route("main-excel")]
    public async Task<IActionResult> ExcelExchangeLincensee(LicenseeHttpParameterExcel httpParameter)
    {
        var excelPackage = await mediator.Send(httpParameter);
        return new EpplusExcelResult(excelPackage);
    }

    [Route("report")]
    public async Task<IActionResult> Exchange(HttpParameterJson httpParameter)
    {
        var viewModel = await mediator.Send(httpParameter);
        return Ok(viewModel);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(HttpParameterExcel httpParameter)
    {
        var excelPackage = await mediator.Send(httpParameter);
        return new EpplusExcelResult(excelPackage);
    }
}