using MediatR;

namespace SMAWinLoss.Summary
{
    using Microsoft.AspNetCore.Mvc;
    using Olympus.WebApi.Epplus;

    [Route("api/sma-win-loss/summary")]
    [ApiController]
    public class SMAWinLossController : ControllerBase
    {
        private readonly IMediator mediator;

        public SMAWinLossController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("")]
        public async Task<IActionResult> Summary(WinLossHttpParameter httpParameters)
        {
            var viewModel = await mediator.Send(httpParameters);
            return Ok(viewModel);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(WinLossHttpParameterExcel httpParameter)
        {
            var excelPackage = await mediator.Send(httpParameter);
            return new EpplusExcelResult(excelPackage);
        }
    }
}