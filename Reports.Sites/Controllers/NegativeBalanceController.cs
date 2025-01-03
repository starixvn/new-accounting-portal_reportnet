using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NegativeBalance.Services;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class NegativeBalanceController(NegativeBalanceService service) : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.NegativeBalance, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> Index()
    {
        var licenseeSiteList = await service.GetLicensee();
        ViewBag.LicensseSiteList = new SelectList(licenseeSiteList, "LicenseeName", "LicenseeName");

        return View();
    }
}