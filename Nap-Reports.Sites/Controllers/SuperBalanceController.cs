using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Sites.Settings;

namespace Nap_Reports.Sites.Controllers
{
    public class SuperBalanceController : Controller
    {
        [NapAuth(FunctionKey = FunctionKeys.SuperBalance, Policy = Constant.Policy.Read)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
