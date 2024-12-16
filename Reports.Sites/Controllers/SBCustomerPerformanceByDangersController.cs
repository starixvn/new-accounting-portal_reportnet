using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Sites.Settings;

namespace Nap_Reports.Sites.Controllers
{
    public class SBCustomerPerformanceByDangersController : Controller
    {
        [NapAuth(FunctionKey = FunctionKeys.SBCustomerPerformanceByDangers, Policy = Constant.Policy.Read)]
        public IActionResult Index()
        {
            return View("~/wwwroot/sb-customer-performance-by-danger/index.cshtml");
        }
    }
}
