using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

using Fanex.NAP.Client;
using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Model;
using SoccerPerformanceDetail.Services;
using Utilities.Configurations;

public class SoccerPerformanceDetailController(SoccerPerformanceDetailService service,
    INapSession napSession,
    ConfigurationService configurationService,
    PermissionService permissionService) : Controller
{
    private readonly int[] ForecastBetType = [1, 3, 7, 8];
    private const string ViewPath = "~/Views/SoccerPerformanceDetail/";

    [NapAuth(FunctionKey = FunctionKeys.SoccerPerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> BetTypePerformance()
    {
        return await RenderViewWithPermissionCheck("BetTypePerformance");
    }

    [NapAuth(FunctionKey = FunctionKeys.SoccerPerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> LeaguePerformance()
    {
        return await RenderViewWithPermissionCheck("LeaguePerformance");
    }

    [NapAuth(FunctionKey = FunctionKeys.SoccerPerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> TraderPerformance()
    {
        return await RenderViewWithPermissionCheck("TraderPerformance");
    }

    [NapAuth(FunctionKey = FunctionKeys.SoccerPerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> TeamPerformance()
    {
        return await RenderViewWithPermissionCheck("TeamPerformance");
    }

    [NapAuth(FunctionKey = FunctionKeys.SoccerPerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> GradePerformance()
    {
        return await RenderViewWithPermissionCheck("GradePerformance");
    }

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.SoccerPerformanceDetail, Policy = Constant.Policy.Read)]
    public IActionResult GetViewScorePermission()
    {
        var result = permissionService.IsScoreViewingPermission();
        return Json(result);
    }

    private async Task<IActionResult> RenderViewWithPermissionCheck(string viewName)
    {
        if (permissionService.HasNotPermissionToAccessPage())
        {
            return View($"{ViewPath}NoPermission.cshtml");
        }
        
        await InitData();
        
        if (viewName == "TeamPerformance")
        {
            var teams = await service.GetTeams();
            ViewBag.Teams = teams;
        }
        
        return View($"{ViewPath}{viewName}.cshtml");
    }

    private async Task InitData()
    {
        var minDate = new DateTime(DateTime.Today.AddYears(configurationService.PerformanceYearRange * -1).Year, 1, 1);
        minDate = minDate < configurationService.AvailableDate ? configurationService.AvailableDate : minDate;

        var betTypePermissions = await permissionService.GetBettypePermission();
        ViewBag.MinDate = minDate;
        ViewBag.DefaultFromDate = DateTime.Today.AddDays(configurationService.PerformanceDefaultDayRange * -1);
        ViewBag.DefaultToDate = DateTime.Today;
        ViewBag.AllowYearRange = configurationService.PerformanceAllowYearRange;
        ViewBag.ForecastBetType = string.Join(",", ForecastBetType);
        ViewBag.Markets = permissionService.GetMarketsPermission();
        ViewBag.IsVirtualPerformancePermission = permissionService.IsVirtualPerformancePermission();
        ViewBag.IsRealPerformancePermission = permissionService.IsRealPerformancePermission();
        ViewBag.GroupBetTypes = await service.GetGroupBetTypes(napSession.UserId, betTypePermissions);
        ViewBag.LeagueGroups = await service.GetLeagueGroups();
        ViewBag.IsExcludeLeagues = (await service.GetLeagueExclusionConfig()).IsExcludeLeagues;
    }
}
