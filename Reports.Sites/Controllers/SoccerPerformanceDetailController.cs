namespace Reports.Sites.Controllers;

using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail;
using SoccerPerformanceDetail.Model;
using SoccerPerformanceDetail.Services;
using SoccerPerformanceDetail.ViewModel;
using Utilities.Configurations;

public class SoccerPerformanceDetailController(SoccerPerformanceDetailService service,
    INapSession napSession,
    ConfigurationService configurationService) : Controller
{
    private readonly int[] ForecastBetType = [1, 3, 7, 8];

    public async Task<IActionResult> BetTypePerformance()
    {
        await InitData();
        return View();
    }

    public async Task<IActionResult> LeaguePerformance()
    {
        await InitData();
        return View();
    }

    public async Task<IActionResult> TraderPerformance()
    {
        await InitData();
        return View();
    }

    public async Task<IActionResult> TeamPerformance()
    {
        await InitData();
        var teams = await service.GetTeams();
        ViewBag.Teams = teams;
        return View();
    }

    public async Task<IActionResult> GradePerformance()
    {
        await InitData();
        return View();
    }

    [HttpGet]
    public IActionResult GetViewScorePermission()
    {
        return Json(true);
    }

    private async Task InitData()
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
    }
}
