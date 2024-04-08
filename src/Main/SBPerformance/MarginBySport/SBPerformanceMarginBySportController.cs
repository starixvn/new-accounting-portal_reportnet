using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SBPerformance.MarginBySport
{
    [Route("api/sb-performance/main")]
    [ApiController]
    public class SBPerformanceMarginBySportController : ControllerBase
    {
        private readonly IMediator mediator;

        public SBPerformanceMarginBySportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("margin-by-sport")]
        public async Task<IActionResult> Sport(MarginBySportHttpParameter httpParameters)
        {
            var viewModel = await mediator.Send(httpParameters);
            return Ok(viewModel);
        }
    }
}