using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SBPerformance.Main
{
    [Route("api/sb-performance/main")]
    [ApiController]
    public class SBPerformanceMainController : ControllerBase
    {
        private readonly IMediator mediator;

        public SBPerformanceMainController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("")]
        public async Task<IActionResult> Main(HttpParameter httpParameters)
        {
            var viewModel = await mediator.Send(httpParameters);
            return Ok(viewModel);
        }
    }
}