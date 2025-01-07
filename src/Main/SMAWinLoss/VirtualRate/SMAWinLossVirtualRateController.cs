namespace SMAWinLoss.VirtualRate;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/sma-win-loss/virtual-rate")]
[ApiController]
public class SMAWinLossVirtualRateController(IMediator mediator) : ControllerBase
{
    [Route("")]
    public async Task<IActionResult> VirtualRate(VirtualRateHttpParameter httpParameters)
    {
        var viewModel = await mediator.Send(httpParameters);
        return Ok(viewModel);
    }
}