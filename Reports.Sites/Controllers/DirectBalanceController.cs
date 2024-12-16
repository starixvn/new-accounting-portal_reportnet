using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;

namespace Nap_Reports.Sites.Controllers
{
    using Utilities;

    public class DirectBalanceController : Controller
    {
        [NapAuth(FunctionKey = AccReportFunctionKeys.DirectBalance, Policy = Constant.Policy.Read)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
