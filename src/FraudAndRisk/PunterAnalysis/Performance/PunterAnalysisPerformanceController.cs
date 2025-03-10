namespace PunterAnalysis.Performance;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sportsbook;
using Summary;

[Route("api/punter-analysis/performance")]
[ApiController]
public class PunterAnalysisPerformanceController(IMediator mediator) : ControllerBase
{
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