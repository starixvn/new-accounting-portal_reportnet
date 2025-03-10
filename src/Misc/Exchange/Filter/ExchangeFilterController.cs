using HttpParameterLicensee = Exchange.Filter.Licensee.HttpParameterJson;
using HttpParameterSport = Exchange.Filter.Sport.HttpParameterJson;

namespace Exchange.Filter;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/exchange/filter")]
[ApiController]
public class ExchangeFilterController(IMediator mediator) : ControllerBase
{
    [Route("licensee")]
    public async Task<IActionResult> Licensee()
    {
        var viewModel = await mediator.Send(new HttpParameterLicensee());
        return Ok(viewModel);
    }


    [Route("sport")]
    public async Task<IActionResult> Sport()
    {
        var viewModel = await mediator.Send(new HttpParameterSport());
        return Ok(viewModel);
    }
}