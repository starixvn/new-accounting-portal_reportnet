using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SBPerformance.TopWinningMember
{
    [Route("api/sb-performance/main")]
    [ApiController]
    public class SBPerformanceTopWinningMemberController : ControllerBase
    {
        private readonly IMediator mediator;

        public SBPerformanceTopWinningMemberController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("top-winning-member")]
        public async Task<IActionResult> Member(TopWinningMemberHttpParameter httpParameters)
        {
            var viewModel = await mediator.Send(httpParameters);
            return Ok(viewModel);
        }
    }
}