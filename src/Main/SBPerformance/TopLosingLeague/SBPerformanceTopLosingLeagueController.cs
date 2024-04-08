using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SBPerformance.TopLosingLeague
{
    [Route("api/sb-performance/main")]
    [ApiController]
    public class SBPerformanceTopLosingLeagueController : ControllerBase
    {
        private readonly IMediator mediator;

        public SBPerformanceTopLosingLeagueController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("top-losing-league")]
        public async Task<IActionResult> League(TopLosingLeagueHttpParameter httpParameters)
        {
            var viewModel = await mediator.Send(httpParameters);
            return Ok(viewModel);
        }
    }
}