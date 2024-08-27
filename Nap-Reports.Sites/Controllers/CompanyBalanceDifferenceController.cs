using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Utilities;

namespace Nap_Reports.Sites.Controllers
{
    public class CompanyBalanceDifferenceController : Controller
    {
        [NapAuth(FunctionKey = AccReportFunctionKeys.CompanyBalanceDifference, Policy = Constant.Policy.Read)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
