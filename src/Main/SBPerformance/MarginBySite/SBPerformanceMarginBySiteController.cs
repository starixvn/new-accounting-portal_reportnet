using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SBPerformance.MarginBySite
{
    [Route("api/sb-performance/main")]
    [ApiController]
    public class SBPerformanceMarginBySiteController : ControllerBase
    {
        private readonly IMediator mediator;

        public SBPerformanceMarginBySiteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("margin-by-site")]
        public async Task<IActionResult> Site(MarginBySiteHttpParameter httpParameters)
        {
            var viewModel = await mediator.Send(httpParameters);
            return Ok(viewModel);
        }
    }
}