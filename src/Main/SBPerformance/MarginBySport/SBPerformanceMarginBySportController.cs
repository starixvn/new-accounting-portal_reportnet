using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SBPerformance.MarginBySport;

[Route("api/sb-performance/main")]
[ApiController]
public class SBPerformanceMarginBySportController(IMediator mediator) : ControllerBase
{
    [Route("margin-by-sport")]
    public async Task<IActionResult> Sport(MarginBySportHttpParameter httpParameters)
    {
        var viewModel = await mediator.Send(httpParameters);
        return Ok(viewModel);
    }
}