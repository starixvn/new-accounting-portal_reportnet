using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace UpdatedUsernameCredit.UpdatedCredit;

using Utilities.ExcelResult;

[Route("api/updated-credit")]
[ApiController]
public class UpdatedCreditController(IMediator mediator) : ControllerBase
{
    [Route("query")]
    public async Task<IActionResult> Query(Message message)
    {
        var viewModel = await mediator.Send(message);
        return Ok(viewModel);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelMessage message)
    {
        var excelPackage = await mediator.Send(message);
        return new EpplusExcelResult(excelPackage);
    }
}