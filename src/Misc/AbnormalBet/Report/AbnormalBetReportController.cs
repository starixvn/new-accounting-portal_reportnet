namespace AbnormalBet.Report;

using HttpParameter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/ab-normal-bet/report")]
[ApiController]
public class AbnormalBetReportController(IMediator mediator) : ControllerBase
{
    [Route("member")]
    public async Task<IActionResult> Member(MemberHttpParameter httpParameters)
    {
        var viewModel = await mediator.Send(httpParameters);
        return Ok(viewModel);
    }

    [Route("licensee")]
    public async Task<IActionResult> Licensee(LicenseeHttpParameter httpParameters)
    {
        var viewModel = await mediator.Send(httpParameters);
        return Ok(viewModel);
    }

    [Route("licensee-excel")]
    public async Task<IActionResult> LicenseeExcel(LicenseeHttpParameterExcel httpParameter)
    {
        var excelPackage = await mediator.Send(httpParameter);
        return new EpplusExcelResult(excelPackage);
    }

    [Route("member-excel")]
    public async Task<IActionResult> MemberExcel(MemberHttpParameterExcel httpParameter)
    {
        var excelPackage = await mediator.Send(httpParameter);
        return new EpplusExcelResult(excelPackage);
    }
}