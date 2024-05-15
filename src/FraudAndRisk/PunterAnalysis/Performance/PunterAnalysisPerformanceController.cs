using MediatR;
using Microsoft.AspNetCore.Mvc;
using PunterAnalysis.Performance.Sportsbook;
using PunterAnalysis.Performance.Summary;

namespace PunterAnalysis.Performance
{
    [Route("api/punter-analysis/performance")]
    [ApiController]
    public class PunterAnalysisPerformanceController : ControllerBase
    {
        private readonly IMediator mediator;

        public PunterAnalysisPerformanceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("summary")]
        public async Task<IActionResult> Summary(SummaryHttpParameterJson parameter)
        {
            return await Handle(parameter);
        }

        [Route("sportsbook")]
        public async Task<IActionResult> Sportsbook(SportsbookHttpParameterJson parameter)
        {
            return await Handle(parameter);
        }

        private async Task<IActionResult> Handle<TResponse>(IRequest<TResponse> parameter)
        {
            var viewModel = await mediator.Send(parameter);
            return Ok(viewModel);
        }
    }
}