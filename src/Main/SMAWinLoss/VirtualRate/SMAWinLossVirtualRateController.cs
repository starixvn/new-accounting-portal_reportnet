using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UniversalReports.SMAWinLoss.VirtualRate
{
    [Route("api/sma-win-loss/virtual-rate")]
    [ApiController]
    public class SMAWinLossVirtualRateController : ControllerBase
    {
        private readonly IMediator mediator;

        public SMAWinLossVirtualRateController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("")]
        public async Task<IActionResult> VirtualRate(VirtualRateHttpParameter httpParameters)
        {
            var viewModel = await mediator.Send(httpParameters);
            return Ok(viewModel);
        }
    }
}