using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client.Helpers.Web;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Sites.Services;
using Nap_Reports.Sites.Settings;
using Nap_Reports.Utilities.BalanceUpServices;

namespace Nap_Reports.Sites.Controllers
{
    public class HouseWinLossByProductsController : Controller
    {
        private readonly IUserService userService;

        private readonly INapSession napSession;

        private readonly INapSettings napSettings;

        private readonly PermissionFilterService permissionService;

        private readonly BalanceUpService balanceUpService;

        public HouseWinLossByProductsController(IUserService userService,
            INapSession napSession,
            INapSettings napSettings,
            PermissionFilterService permissionService,
            BalanceUpService balanceUpService)
        {
            this.userService = userService;
            this.napSession = napSession;
            this.napSettings = napSettings;
            this.permissionService = permissionService;
            this.balanceUpService = balanceUpService;
        }

        public IActionResult Index()
        {
            ViewBag.RootUrl = string.Empty;
            ViewBag.LegacyPath = string.Empty;
            ViewBag.BalanceUp = balanceUpService.Query();

            var productPermissions = this.userService.GetPermissions(
                napSession.UserId,
                napSettings.SiteId,
                FunctionKeys.HouseWinLossProductPermission)
                .Where(p => p.Policy >= (int)Constant.Policy.Read)
                .OrderBy(x => x.Name)
                .Select(p => p.Key);

            ViewBag.ProductPermissions = productPermissions;
            var permission = productPermissions.Any() ? productPermissions.FirstOrDefault() : string.Empty;
            ViewBag.PermissionProfileID = permissionService.GetPermissionProfileId(permission);
            ViewBag.ModePermissions = this.userService.GetPermissions(
                napSession.UserId,
                napSettings.SiteId,
                FunctionKeys.HouseWinLossModePermission)
                .Where(p => p.Policy >= (int)Constant.Policy.Read)
                .Select(p => p.Key);
            return View();
        }
    }
}
