using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Utilities;

namespace Nap_Reports.Sites.Controllers
{
    public class CompanyWinLossController : Controller
    {
        [NapAuth(FunctionKey = AccReportFunctionKeys.CompanyWinLoss, Policy = Constant.Policy.Read)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
