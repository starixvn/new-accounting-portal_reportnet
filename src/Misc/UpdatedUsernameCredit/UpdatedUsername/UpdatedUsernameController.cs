using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;

namespace UpdatedUsernameCredit.UpdatedUsername;

[Route("api/updated-username")]
[ApiController]
public class UpdatedUsernameController(IMediator mediator) : ControllerBase
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