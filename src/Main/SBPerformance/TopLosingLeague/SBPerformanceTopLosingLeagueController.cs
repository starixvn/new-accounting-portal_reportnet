using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SBPerformance.TopLosingLeague;

[Route("api/sb-performance/main")]
[ApiController]
public class SBPerformanceTopLosingLeagueController(IMediator mediator) : ControllerBase
{
    [Route("top-losing-league")]
    public async Task<IActionResult> League(TopLosingLeagueHttpParameter httpParameters)
    {
        var viewModel = await mediator.Send(httpParameters);
        return Ok(viewModel);
    }
}