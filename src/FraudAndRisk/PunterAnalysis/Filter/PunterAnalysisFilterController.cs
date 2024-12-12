using MediatR;
using Microsoft.AspNetCore.Mvc;
using HttpParameterAvailableDatabase = PunterAnalysis.Filter.AvailableDatabase.HttpParameterJson;
using HttpParameterAvailableDataRange = PunterAnalysis.Filter.AvailableDateRange.HttpParameterJson;
using HttpParameterLeagueGroup = PunterAnalysis.Filter.LeagueGroup.HttpParameterJson;
using HttpParameterProductSportBetType = PunterAnalysis.Filter.ProductSportBetType.HttpParameterJson;
using HttpParameterSportsbookSportBetType = PunterAnalysis.Filter.SportsbookSportBetType.HttpParameterJson;
using HttpParameterSportTypeLeague = PunterAnalysis.Filter.SportTypeLeague.HttpParameterJson;
using HttpParameterUsernameSuggestion = PunterAnalysis.Filter.UsernameSuggestion.HttpParameterJson;

namespace PunterAnalysis.Filter
{
    [Route("api/punter-analysis/filter")]
    [ApiController]
    public class PunterAnalysisFilterController(IMediator mediator) : ControllerBase
    {
        [Route("available-database")]
        public async Task<IActionResult> AvailableDatabase(HttpParameterAvailableDatabase parameter)
        {
            return await Handle(parameter);
        }

        [Route("available-date-range")]
        public async Task<IActionResult> AvailableDateRange(HttpParameterAvailableDataRange parameter)
        {
            return await Handle(parameter);
        }

        [Route("product-sport-bet-type")]
        public async Task<IActionResult> ProductSportBetType(HttpParameterProductSportBetType parameter)
        {
            return await Handle(parameter);
        }

        [Route("username-suggestion")]
        public async Task<IActionResult> UsernameSuggestion(HttpParameterUsernameSuggestion parameter)
        {
            return await Handle(parameter);
        }

        [Route("sportsbook-sport-bet-type")]
        public async Task<IActionResult> SportsbookSportBetType(HttpParameterSportsbookSportBetType parameter)
        {
            return await Handle(parameter);
        }

        [Route("sport-type-league")]
        public async Task<IActionResult> SportTypeLeague(HttpParameterSportTypeLeague parameter)
        {
            return await Handle(parameter);
        }

        [Route("league-group")]
        public async Task<IActionResult> LeagueGroup(HttpParameterLeagueGroup parameter)
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