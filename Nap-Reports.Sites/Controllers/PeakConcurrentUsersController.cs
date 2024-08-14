using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PeakConcurrentUsers.Services;

namespace Nap_Reports.Sites.Controllers
{
    public class PeakConcurrentUsersController(PeakConcurrentUserService service) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var licensee = await service.GetLicensee();
            ViewBag.LicensseSiteList = new SelectList(licensee, "LicenseeName", "LicenseeName");
            return View();
        }
    }
}
