namespace SBCustomerPerformanceByDanger.Filter;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/sb-customer-performance-by-danger/filter")]
[ApiController]
public class CustomerPerformanceByDangerFilterController(IMediator mediator) : ControllerBase
{
    [Route("balance-up-date")]
    public async Task<IActionResult> BalanceUpDate()
    {
        var viewModel = await mediator.Send(new BalanceUpDateQueryHttpParameter());
        return Ok(viewModel);
    }

    [Route("sport")]
    public async Task<IActionResult> Sport()
    {
        var viewModel = await mediator.Send(new SportQueryHttpParameter());
        return Ok(viewModel);
    }

    [Route("division")]
    public async Task<IActionResult> Division()
    {
        var viewModel = await mediator.Send(new DivisionQueryHttpParameter());
        return Ok(viewModel);
    }

    [Route("league-group")]
    public async Task<IActionResult> LeagueGroup(LeagueGroupQueryHttpParameter parameter)
    {
        var viewModel = await mediator.Send(parameter);
        return Ok(viewModel);
    }

    [Route("league-suggestion")]
    public async Task<IActionResult> League(LeagueQueryHttpParameter parameter)
    {
        var viewModel = await mediator.Send(parameter);
        return Ok(viewModel);
    }
}