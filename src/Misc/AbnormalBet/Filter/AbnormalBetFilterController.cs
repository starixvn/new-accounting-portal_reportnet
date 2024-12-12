using AbnormalBet.HttpParameter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbnormalBet.Filter
{
    [Route("api/ab-normal-bet/filter")]
    [ApiController]
    public class AbnormalBetFilterController(IMediator mediator) : ControllerBase
    {
        [Route("sport")]
        public async Task<IActionResult> Sport()
        {
            var viewModel = await mediator.Send(new SportHttpParameter());
            return Ok(viewModel);
        }

        [Route("reason")]
        public async Task<IActionResult> Reason()
        {
            var viewModel = await mediator.Send(new ReasonHttpParameter());
            return Ok(viewModel);
        }

        [Route("site")]
        public async Task<IActionResult> Site()
        {
            var viewModel = await mediator.Send(new SiteHttpParameter());
            return Ok(viewModel);
        }

        [Route("bet-type")]
        public async Task<IActionResult> BetType(BetTypeHttpParameter httpParameter)
        {
            var viewModel = await mediator.Send(httpParameter);
            return Ok(viewModel);
        }

        [Route("currency")]
        public async Task<IActionResult> Currency()
        {
            var viewModel = await mediator.Send(new CurrencyHttpParameter());
            return Ok(viewModel);
        }

        [Route("league")]
        public async Task<IActionResult> League(LeagueHttpParameter httpParameter)
        {
            var viewModel = await mediator.Send(httpParameter);
            return Ok(viewModel);
        }
    }
}