using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Utilities;
using Utilities.BalanceUpServices;

namespace Nap_Reports.Sites.Controllers
{
    public class DuplicateTicketsController(BalanceUpService balanceUpService) : Controller
    {
        [NapAuth(FunctionKey = AccReportFunctionKeys.DuplicateTickets, Policy = Constant.Policy.Read)]
        public IActionResult Index()
        {
            var balanceUpDate = balanceUpService.Query();
            ViewBag.MoveBetsDate = balanceUpDate.MoveBetsDate;

            return View();
        }
    }
}
