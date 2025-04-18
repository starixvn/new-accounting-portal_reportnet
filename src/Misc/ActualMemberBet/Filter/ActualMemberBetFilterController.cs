namespace ActualMemberBet.Filter;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/actual-member-bet/filter")]
[ApiController]
public class ActualMemberBetFilterController(IMediator mediator) : ControllerBase
{
    [Route("currency")]
    public async Task<IActionResult> Currency()
    {
        var viewModel = await mediator.Send(new CurrencyHttpParameter());
        return Ok(viewModel);
    }

    [Route("odds")]
    public async Task<IActionResult> Odds()
    {
        var viewModel = await mediator.Send(new OddsHttpParameter());
        return Ok(viewModel);
    }

    [Route("site")]
    public async Task<IActionResult> Site()
    {
        var viewModel = await mediator.Send(new SiteHttpParameter());
        return Ok(viewModel);
    }
}