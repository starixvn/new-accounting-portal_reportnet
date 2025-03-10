namespace SMAWinLoss.Filter;

using Fanex.NAP.Client;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/sma-win-loss/filter")]
[ApiController]
public class SMAWinLossFilterController(
    IMediator mediator,
    INapSession napSession,
    INapSettings napSettings,
    IUserService userService)
    : ControllerBase
{
    [Route("currency")]
    public async Task<IActionResult> Currency()
    {
        var viewModel = await mediator.Send(new CurrencyHttpParameter());
        return Ok(viewModel);
    }

    [Route("site")]
    public async Task<IActionResult> Site()
    {
        var viewModel = await mediator.Send(new SiteHttpParameter());
        return Ok(viewModel);
    }

    [Route("sport")]
    public async Task<IActionResult> Sport()
    {
        var viewModel = await mediator.Send(new SportHttpParameter());
        return Ok(viewModel);
    }

    [Route("league-group")]
    public async Task<IActionResult> LeagueGroup(LeagueGroupHttpParameter parameter)
    {
        var viewModel = await mediator.Send(parameter);
        return Ok(viewModel);
    }

    [Route("available-database")]
    public async Task<IActionResult> AvailableDatabase()
    {
        var viewModel = await mediator.Send(new AvailableDatabaseHttpParameter());
        return Ok(viewModel);
    }

    [Route("available-date-data")]
    public async Task<IActionResult> AvailableDateData()
    {
        var viewModel = await mediator.Send(new AvailableDateDataHttpParameter());
        return Ok(viewModel);
    }

    [Route("bet-from")]
    public async Task<IActionResult> BetFrom()
    {
        var viewModel = await mediator.Send(new BetFromHttpParameter());
        return Ok(viewModel);
    }

    [Route("permission")]
    public async Task<IActionResult> Permission()
    {
        var permissions = (await userService.GetPermissionsAsync(
                napSession.UserId,
                napSettings.SiteId,
                "SMAWinLossSummary"))
            .Where(p => p.Policy >= (int)Constant.Policy.Read)
            .OrderBy(x => x.Name)
            .Select(p => p.Key);

        var permission = permissions.Any() ? permissions.FirstOrDefault() : string.Empty;

        var permissionHttpParameter = new PermissionHttpParameter
        { 
            FunctionKey = permission 
        };

        var viewModel = await mediator.Send(permissionHttpParameter);
        return Ok(viewModel);
    }
}