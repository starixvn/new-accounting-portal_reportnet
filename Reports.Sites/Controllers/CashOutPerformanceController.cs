using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Sites.Settings;

namespace Nap_Reports.Sites.Controllers
{
    public class CashOutPerformanceController : Controller
    {
        [NapAuth(FunctionKey = FunctionKeys.CashOutPerformance, Policy = Constant.Policy.Read)]
        public ActionResult Index()
        {
            return View("~/wwwroot/cash-out-performance/index.cshtml");
        }
    }
}
