using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client.Services;
using LeagueWinLoss.Services;
using Microsoft.AspNetCore.Mvc;
using Utilities.BalanceUpServices;

namespace Reports.Sites.Controllers;

using Fanex.NAP.Client;
using Utilities;

public class LeagueWinLossController(
    INapSettings napSettings,
    INapSession napSession,
    IUserService userService,
    BalanceUpService balanceUpService,
    LeagueWinLossService service) : Controller
{
    private const string LeagueWinLoss_NormalMode = "LeagueWinLoss_NormalMode";

    [NapAuth(FunctionKey = AccReportFunctionKeys.LeagueWinLoss, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        var balanceUp = balanceUpService.Query();
        var modePermission = userService.GetPermissions(
                napSession.UserId,
                napSettings.SiteId,
                AccReportFunctionKeys.LeagueWinLossModePermission)
            .Where(p => p.Policy >= (int)Constant.Policy.Read)
            .Select(p => p.Key);

        var productPermission = userService.GetPermissions(
                napSession.UserId,
                napSettings.SiteId,
                AccReportFunctionKeys.LeagueWinLossProductPermission)
            .Where(p => p.Policy >= (int)Constant.Policy.Read)
            .OrderBy(x => x.Name)
            .Select(p => p.Key);

        var permission = productPermission.Any() ? productPermission.FirstOrDefault() : string.Empty;
        ViewBag.PermissionProfileID = service.GetPermissionProfileId(permission);

        ViewBag.IsFullModePermission = modePermission.Count() > 1;
        ViewBag.IsNormalMode = modePermission.FirstOrDefault() == LeagueWinLoss_NormalMode;
        ViewBag.Status = service.GetStatus();
        ViewBag.MinDate = balanceUp.MoveBetsDate.AddDays(1).ToShortDateString();
        return View();
    }
}