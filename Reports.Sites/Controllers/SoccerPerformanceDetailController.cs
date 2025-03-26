namespace Reports.Sites.Controllers;

using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail;
using SoccerPerformanceDetail.Model;
using SoccerPerformanceDetail.Services;
using Utilities.Configurations;

public class SoccerPerformanceDetailController(SoccerPerformanceDetailService service,
    INapSession napSession,
    ConfigurationService configurationService) : Controller
{
    private readonly int[] ForecastBetType = [1, 3, 7, 8];

    public async Task<IActionResult> BetTypePerformance()
    {
        var minDate = new DateTime(DateTime.Today.AddYears(configurationService.PerformanceYearRange * -1).Year, 1, 1);
        minDate = minDate < configurationService.AvailableDate ? configurationService.AvailableDate : minDate;

        ViewBag.MinDate = minDate;
        ViewBag.DefaultFromDate = DateTime.Today.AddDays(configurationService.PerformanceDefaultDayRange * -1);
        ViewBag.DefaultToDate = DateTime.Today;
        ViewBag.AllowYearRange = configurationService.PerformanceAllowYearRange;
        ViewBag.ForecastBetType = string.Join(",", ForecastBetType);
        ViewBag.Markets = new Dictionary<Market, string>
        {
            { Market.All, "All" },
            { Market.Live, "Live" },
            { Market.NonLive, "Non-Live" }
        };
        ViewBag.IsVirtualPerformancePermission = true;
        ViewBag.IsRealPerformancePermission = true;
        ViewBag.GroupBetTypes = await service.GetGroupBetTypes(napSession.UserId, BettypeViewPermission.All);
        ViewBag.LeagueGroups = await service.GetLeagueGroups();
        return View();
    }

    public async Task<IActionResult> LeaguePerformance()
    {
        var minDate = new DateTime(DateTime.Today.AddYears(configurationService.PerformanceYearRange * -1).Year, 1, 1);
        minDate = minDate < configurationService.AvailableDate ? configurationService.AvailableDate : minDate;

        ViewBag.MinDate = minDate;
        ViewBag.DefaultFromDate = DateTime.Today.AddDays(configurationService.PerformanceDefaultDayRange * -1);
        ViewBag.DefaultToDate = DateTime.Today;
        ViewBag.AllowYearRange = configurationService.PerformanceAllowYearRange;
        ViewBag.ForecastBetType = string.Join(",", ForecastBetType);
        ViewBag.Markets = new Dictionary<Market, string>
        {
            { Market.All, "All" },
            { Market.Live, "Live" },
            { Market.NonLive, "Non-Live" }
        };
        ViewBag.IsVirtualPerformancePermission = true;
        ViewBag.IsRealPerformancePermission = true;
        ViewBag.GroupBetTypes = await service.GetGroupBetTypes(napSession.UserId, BettypeViewPermission.All);
        ViewBag.LeagueGroups = await service.GetLeagueGroups();
        return View();
    }
}
