using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;

namespace UpdatedUsernameCredit.UpdatedCredit
{
    [Route("api/updated-credit")]
    [ApiController]
    public class UpdatedCreditController : ControllerBase
    {
        private readonly IMediator mediator;

        public UpdatedCreditController(IMediator mediator)
        {
            this.mediator = mediator;
        }

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
}