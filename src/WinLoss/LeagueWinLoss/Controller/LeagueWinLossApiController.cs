using LeagueWinLoss.Criteria;
using LeagueWinLoss.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeagueWinLoss.Controller
{
    [Route("api/league-win-loss")]

    [ApiController]
    public class LeagueWinLossApiController(LeagueWinLossService service,
        LeagueWinLossExcelService excelService,
        MatchWinLossExcelService matchExcelService,
        BetTypeWinLossExcelService betTypeExcelService,
        SiteWinLossExcelService siteExcelService,
        SuperWinLossExcelService superExcelService) : ControllerBase
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

        [Route("export-league-win-loss")]
        public async Task<IActionResult> ExportExcelLeagueWinLoss(LeagueWinLossCriteria criteria)
        {
            var data = await service.GetLeagueWinLoss(criteria);
            var title =
                $"League Win Loss Report From {criteria.StartDate: MMM dd, yyyy} To {criteria.EndDate: MMM dd, yyyy}";
            var excelPackage = await excelService.ExportExcelLeagueWinLoss(data, title);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"League_Win_Loss_Report_From_Date_{criteria.StartDate: MM_dd_yyyy}_To_{criteria.EndDate: MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
        }

        [Route("bettypes")]
        public async Task<IActionResult> GetBetTypes(BetTypesCriteria criteria)
        {
            var result = await service.GetBetTypes(criteria);
            return Ok(result);
        }

        [Route("export-bettypes")]
        public async Task<IActionResult> ExportExcelBetTypes(BetTypesCriteria criteria)
        {
            var data = await service.GetBetTypes(criteria);
            var title =
                $"League Win Loss Report From {criteria.StartDate: MMM dd, yyyy} To {criteria.EndDate: MMM dd, yyyy}";
            var header = $"{criteria.SportName} - {criteria.LeagueName} - {criteria.MatchEvent}";
            var excelPackage = await betTypeExcelService.ExportExcel(data, title, header);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"League_Win_Loss_Report_From_Date_{criteria.StartDate: MM_dd_yyyy}_To_{criteria.EndDate: MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
        }

        [Route("bettypes-by-status")]
        public async Task<IActionResult> GetBetTypesByStatus(BetTypesCriteria criteria)
        {
            var result = await service.GetBetTypesByLiveNonLiveStatus(criteria);
            return Ok(result);
        }

        [Route("export-bettypes-by-status")]
        public async Task<IActionResult> ExportExcelBetTypesByStatus(BetTypesCriteria criteria)
        {
            var data = await service.GetBetTypesByLiveNonLiveStatus(criteria);
            var title =
                $"League Win Loss Report From {criteria.StartDate: MMM dd, yyyy} To {criteria.EndDate: MMM dd, yyyy}";
            var header = $"{criteria.SportName} - {criteria.LeagueName} - {criteria.MatchEvent}";
            var excelPackage = await betTypeExcelService.ExportExcelBetTypeByStatus(data, title, header);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"League_Win_Loss_Report_From_Date_{criteria.StartDate: MM_dd_yyyy}_To_{criteria.EndDate: MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
        }

        [Route("matches")]
        public async Task<IActionResult> GetMatches(MatchesCriteria criteria)
        {
            var result = await service.GetMatches(criteria);
            return Ok(result);
        }

        [Route("export-matches")]
        public async Task<IActionResult> ExportExcelMatches(MatchesCriteria criteria)
        {
            var data = await service.GetMatches(criteria);
            var title =
                $"League Win Loss Report From {criteria.StartDate: MMM dd, yyyy} To {criteria.EndDate: MMM dd, yyyy}";
            var header = $"{criteria.SportName} - {criteria.LeagueName}";
            var excelPackage = await matchExcelService.ExportExcel(data, title, header);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"League_Win_Loss_Report_From_Date_{criteria.StartDate: MM_dd_yyyy}_To_{criteria.EndDate: MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
        }

        [Route("matches-from-bettype-status")]
        public async Task<IActionResult> GetMatchesFromBetTypeStatus(MatchesCriteria criteria)
        {
            var result = await service.GetMatchesFromBetTypeStatus(criteria);
            return Ok(result);
        }

        [Route("export-matches-from-bettype-status")]
        public async Task<IActionResult> ExportExcelMatchesFromBetTypeStatus(MatchesCriteria criteria)
        {
            var data = await service.GetMatchesFromBetTypeStatus(criteria);
            var title =
                $"League Win Loss Report From {criteria.StartDate: MMM dd, yyyy} To {criteria.EndDate: MMM dd, yyyy}";
            var liveNonLive = criteria.LiveIndicator == 1 ? "Live" : "Non-Live";
            var header = $"{criteria.SportName} - {criteria.LeagueName} - {criteria.BetTypeName} - {liveNonLive}";
            var excelPackage = await matchExcelService.ExportExcelFromBetTypeStatus(data, title, header);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"League_Win_Loss_Report_From_Date_{criteria.StartDate: MM_dd_yyyy}_To_{criteria.EndDate: MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
        }

        [Route("sites")]
        public async Task<IActionResult> GetSites(SitesCriteria criteria)
        {
            var result = await service.GetSites(criteria);
            return Ok(result);
        }

        [Route("export-sites")]
        public async Task<IActionResult> ExportExcelSites(SitesCriteria criteria)
        {
            var data = await service.GetSites(criteria);
            var title =
                $"League Win Loss Report From {criteria.StartDate: MMM dd, yyyy} To {criteria.EndDate: MMM dd, yyyy}";
            var liveNonLive = criteria.LiveIndicator ? "Live" : "Non-Live";
            var header = $"{criteria.SportName} - {criteria.LeagueName} - {criteria.MatchEvent} - {liveNonLive} - {criteria.BetTypeName} - {criteria.SiteType}";
            var excelPackage = await siteExcelService.ExportExcel(data, title, header);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"League_Win_Loss_Report_From_Date_{criteria.StartDate: MM_dd_yyyy}_To_{criteria.EndDate: MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
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

        [Route("export-supers")]
        public async Task<IActionResult> ExportExcelSupers(SuperCriteria criteria)
        {
            var data = await service.GetSupers(criteria);
            var title =
                $"League Win Loss Report From {criteria.StartDate: MMM dd, yyyy} To {criteria.EndDate: MMM dd, yyyy}";
            var liveNonLive = criteria.LiveIndicator ? "Live" : "Non-Live";
            var header = $"{criteria.SportName} - {criteria.LeagueName} - {criteria.MatchEvent} - {liveNonLive} - {criteria.BetTypeName} - {criteria.SiteType} - {criteria.SiteName}";
            var excelPackage = await superExcelService.ExportExcel(data, title, header);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"League_Win_Loss_Report_From_Date_{criteria.StartDate: MM_dd_yyyy}_To_{criteria.EndDate: MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
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
