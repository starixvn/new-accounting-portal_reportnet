using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nap_Reports.Utilities;
using PeakConcurrentUsers.Services;

namespace Nap_Reports.Sites.Controllers
{
    public class PeakConcurrentUsersController(PeakConcurrentUserService service) : Controller
    {
        [NapAuth(FunctionKey = AccReportFunctionKeys.PeakConcurrentUsers, Policy = Constant.Policy.Read)]
        public async Task<IActionResult> Index()
        {
            var licensee = await service.GetLicensee();
            ViewBag.LicensseSiteList = new SelectList(licensee, "LicenseeName", "LicenseeName");
            return View();
        }
    }
}
