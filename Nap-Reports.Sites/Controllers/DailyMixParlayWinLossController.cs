using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Sites.Settings;
using Utilities.BalanceUpServices;

namespace Nap_Reports.Sites.Controllers
{
    public class DailyMixParlayWinLossController(BalanceUpService balanceUpService) : Controller
    {
        [NapAuth(FunctionKey = FunctionKeys.DailyMixParlayWinLoss, Policy = Constant.Policy.Read)]
        public IActionResult Index()
        {
            var balanceUp = balanceUpService.Query();

            ViewBag.MoveBetDate = balanceUp.MoveBetsDate;
            return View();
        }
    }
}
