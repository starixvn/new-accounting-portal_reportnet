namespace SBPerformance.MarginBySite;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/sb-performance/main")]
[ApiController]
public class SBPerformanceMarginBySiteController(IMediator mediator) : ControllerBase
{
    [Route("margin-by-site")]
    public async Task<IActionResult> Site(MarginBySiteHttpParameter httpParameters)
    {
        var viewModel = await mediator.Send(httpParameters);
        return Ok(viewModel);
    }
}