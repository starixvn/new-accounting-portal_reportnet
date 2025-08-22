using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;
using TGR.Services;
using Utilities.Configurations;

namespace Reports.Sites.Controllers;

public class PerformanceDetailJuiceLeftoverController(ConfigurationService configurationService,
    PermissionService permissionService,
    DataService dataService,
    TeamService teamService) : Controller
{
    private const string ViewPath = "~/Views/PerformanceDetailJuiceLeftover/";

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
    public async Task<IActionResult> TeamPerformance()
    {
        ViewBag.Teams = await teamService.GetTeams();
        return await RenderViewWithPermissionCheck("TeamPerformance");
    }

    [NapAuth(FunctionKey = FunctionKeys.SoccerPerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> GradePerformance()
    {
        return await RenderViewWithPermissionCheck("GradePerformance");
    }

    [NapAuth(FunctionKey = FunctionKeys.SoccerPerformanceDetail, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> TraderPerformance()
    {
        return await RenderViewWithPermissionCheck("TraderPerformance");
    }

    private async Task<IActionResult> RenderViewWithPermissionCheck(string viewName)
    {
        if (permissionService.HasNotPermissionToAccessPage())
        {
            return View($"{ViewPath}NoPermission.cshtml");
        }

        await InitData();

        return View($"{ViewPath}{viewName}.cshtml");
    }

    private async Task InitData()
    {
        var minDate = new DateTime(DateTime.Today.AddYears(configurationService.PerformanceYearRange * -1).Year, 1, 1);
        minDate = minDate < configurationService.AvailableDate ? configurationService.AvailableDate : minDate;

        ViewBag.MinDate = minDate;
        ViewBag.DefaultFromDate = DateTime.Today.AddDays(configurationService.PerformanceDefaultDayRange * -1);
        ViewBag.DefaultToDate = DateTime.Today;
        ViewBag.AllowYearRange = configurationService.PerformanceAllowYearRange;
        ViewBag.IsExcludeLeagues = (await dataService.GetLeagueExclusionConfig()).IsExcludeLeagues;
    }
}