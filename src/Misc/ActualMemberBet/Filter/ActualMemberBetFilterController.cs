using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ActualMemberBet.Filter
{
    [Route("api/actual-member-bet/filter")]
    [ApiController]
    public class ActualMemberBetFilterController : ControllerBase
    {
        private readonly IMediator mediator;

        public ActualMemberBetFilterController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("currency")]
        public async Task<IActionResult> Currency()
        {
            var viewModel = await mediator.Send(new CurrencyHttpParameter());
            return Ok(viewModel);
        }

        [Route("odds")]
        public async Task<IActionResult> Odds()
        {
            var viewModel = await mediator.Send(new OddsHttpParameter());
            return Ok(viewModel);
        }

        [Route("site")]
        public async Task<IActionResult> Site()
        {
            var viewModel = await mediator.Send(new SiteHttpParameter());
            return Ok(viewModel);
        }
    }
}
