namespace SBPerformance.TopWinningMember;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/sb-performance/main")]
[ApiController]
public class SBPerformanceTopWinningMemberController(IMediator mediator) : ControllerBase
{
    [Route("top-winning-member")]
    public async Task<IActionResult> Member(TopWinningMemberHttpParameter httpParameters)
    {
        var viewModel = await mediator.Send(httpParameters);
        return Ok(viewModel);
    }
}