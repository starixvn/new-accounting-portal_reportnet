using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nap_Reports.Sites.Settings;
using NegativeBalance.Model;
using NegativeBalance.Services;

namespace Nap_Reports.Sites.Controllers
{
    public class NegativeBalanceController(NegativeBalanceService service) : Controller
    {
        [NapAuth(FunctionKey = FunctionKeys.NegativeBalance, Policy = Constant.Policy.Read)]
        public async Task<IActionResult> Index()
        {
            var licenseeSiteList = await service.GetLicensee();
            var all = new LicenseeModel { LicenseeName = "All", LicenseeId = -1 };
            licenseeSiteList.Insert(0, all);
            ViewBag.LicensseSiteList = new SelectList(licenseeSiteList, "LicenseeName", "LicenseeName");

            return View();
        }
    }
}
