using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

using Fanex.NAP.Client;
using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail;
using SoccerPerformanceDetail.Model;
using SoccerPerformanceDetail.Services;
using Utilities.Configurations;

public class SoccerPerformanceDetailController(SoccerPerformanceDetailService service,
    INapSession napSession,
    ConfigurationService configurationService,
    PermissionService permissionService) : Controller
{
    private readonly int[] ForecastBetType = [1, 3, 7, 8];

    [NapAuth(FunctionKey = FunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> BetTypePerformance()
    {
        if (permissionService.HasNotPermissionToAccessPage())
        {
            return View("~/Views/SoccerPerformanceDetail/NoPermission.cshtml");
        }
        await InitData();
        return View();
    }

    [NapAuth(FunctionKey = FunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> LeaguePerformance()
    {
        if (permissionService.HasNotPermissionToAccessPage())
        {
            return View("~/Views/SoccerPerformanceDetail/NoPermission.cshtml");
        }
        await InitData();
        return View();
    }

    [NapAuth(FunctionKey = FunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> TraderPerformance()
    {
        if (permissionService.HasNotPermissionToAccessPage())
        {
            return View("~/Views/SoccerPerformanceDetail/NoPermission.cshtml");
        }
        await InitData();
        return View();
    }

    [NapAuth(FunctionKey = FunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> TeamPerformance()
    {
        if (permissionService.HasNotPermissionToAccessPage())
        {
            return View("~/Views/SoccerPerformanceDetail/NoPermission.cshtml");
        }
        await InitData();
        var teams = await service.GetTeams();
        ViewBag.Teams = teams;
        return View();
    }

    [NapAuth(FunctionKey = FunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> GradePerformance()
    {
        if (permissionService.HasNotPermissionToAccessPage())
        {
            return View("~/Views/SoccerPerformanceDetail/NoPermission.cshtml");
        }
        await InitData();
        return View();
    }

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
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
        ViewBag.Markets = permissionService.GetMarketsPermission();
        ViewBag.IsVirtualPerformancePermission = permissionService.IsVirtualPerformancePermission();
        ViewBag.IsRealPerformancePermission = permissionService.IsRealPerformancePermission();
        ViewBag.GroupBetTypes = await service.GetGroupBetTypes(napSession.UserId, BettypeViewPermission.All);
        ViewBag.LeagueGroups = await service.GetLeagueGroups();
        ViewBag.IsExcludeLeagues = (await service.GetLeagueExclusionConfig()).IsExcludeLeagues;
    }
}
