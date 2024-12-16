using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SBPerformance.Main;

[Route("api/sb-performance/main")]
[ApiController]
public class SBPerformanceMainController(IMediator mediator) : ControllerBase
{
    [Route("")]
    public async Task<IActionResult> Main(HttpParameter httpParameters)
    {
        var viewModel = await mediator.Send(httpParameters);
        return Ok(viewModel);
    }
}