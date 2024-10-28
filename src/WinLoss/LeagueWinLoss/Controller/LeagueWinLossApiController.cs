using LeagueWinLoss.Criteria;
using LeagueWinLoss.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeagueWinLoss.Controller
{
    [Route("api/league-win-loss")]

    [ApiController]
    public class LeagueWinLossApiController(LeagueWinLossService service) : ControllerBase
    {
        [Route("sport")]
        public async Task<IActionResult> GetSport()
        {
            var result = await service.GetSports();
            return Ok(result);
        }

        [Route("global-showtime")]
        public IActionResult GetGlobalShowTime()
        {
            var result = service.GetGlobalShowTime();
            return Ok(result);
        }

        [Route("site-filter")]
        public async Task<IActionResult> GetSiteFilter()
        {
            var result = await service.GetSiteFilter();
            return Ok(result);
        }

        [Route("league-win-loss")]
        public async Task<IActionResult> GetLeagueWinLoss(LeagueWinLossCriteria criteria)
        {
            var result = await service.GetLeagueWinLoss(criteria);
            return Ok(result);
        }

        [Route("bettypes")]
        public async Task<IActionResult> GetBetTypes(BetTypesCriteria criteria)
        {
            var result = await service.GetBetTypes(criteria);
            return Ok(result);
        }

        [Route("bettypes-by-status")]
        public async Task<IActionResult> GetBetTypesByStatus(BetTypesCriteria criteria)
        {
            var result = await service.GetBetTypesByLiveNonLiveStatus(criteria);
            return Ok(result);
        }

        [Route("matches")]
        public async Task<IActionResult> GetMatches(MatchesCriteria criteria)
        {
            var result = await service.GetMatches(criteria);
            return Ok(result);
        }

        [Route("matches-from-bettype-status")]
        public async Task<IActionResult> GetMatchesFromBetTypeStatus(MatchesCriteria criteria)
        {
            var result = await service.GetMatchesFromBetTypeStatus(criteria);
            return Ok(result);
        }

        [Route("sites")]
        public async Task<IActionResult> GetSites(SitesCriteria criteria)
        {
            var result = await service.GetSites(criteria);
            return Ok(result);
        }

        [Route("virtual-rate")]
        public async Task<IActionResult> GetVirtualRate(VirtualRateCriteria criteria)
        {
            var result = await service.GetVirtualRate(criteria);
            return Ok(result);
        }

        [Route("supers")]
        public async Task<IActionResult> GetSupers(SuperCriteria criteria)
        {
            var result = await service.GetSupers(criteria);
            return Ok(result);
        }

        [Route("league-suggestion")]
        [HttpGet]
        public async Task<IActionResult> GetLeagueSuggestion(string sportBettype, string query)
        {
            var criteria = new LeagueSuggestionCriteria
            {
                SportBettype = sportBettype,
                SearchText = query
            };
            var result = await service.GetLeagueSuggestion(criteria);
            return Ok(result);
        }
    }
}
