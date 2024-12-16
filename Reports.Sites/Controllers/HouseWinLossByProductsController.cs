using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;
using Utilities.BalanceUpServices;
using Utilities.PermissionFilter;

namespace Reports.Sites.Controllers;

using Fanex.NAP.Client;

public class HouseWinLossByProductsController(
    IUserService userService,
    INapSession napSession,
    INapSettings napSettings,
    PermissionFilterService permissionService,
    BalanceUpService balanceUpService)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.RootUrl = string.Empty;
        ViewBag.LegacyPath = string.Empty;
        ViewBag.BalanceUp = balanceUpService.Query();

        var productPermissions = (await userService.GetPermissionsAsync(
                napSession.UserId,
                napSettings.SiteId,
                FunctionKeys.HouseWinLossProductPermission))
            .Where(p => p.Policy >= (int)Constant.Policy.Read)
            .OrderBy(x => x.Name)
            .Select(p => p.Key)
            .ToList();

        ViewBag.ProductPermissions = productPermissions;
        var permission = productPermissions.Any() ? productPermissions.FirstOrDefault() : string.Empty;
        ViewBag.PermissionProfileID = await permissionService.GetPermissionProfileId(permission);
        ViewBag.ModePermissions = (await userService.GetPermissionsAsync(
                napSession.UserId,
                napSettings.SiteId,
                FunctionKeys.HouseWinLossModePermission))
            .Where(p => p.Policy >= (int)Constant.Policy.Read)
            .Select(p => p.Key)
            .ToList();
        return View();
    }
}