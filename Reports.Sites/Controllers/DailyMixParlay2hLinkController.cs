using Microsoft.AspNetCore.Mvc;
using Utilities.BalanceUpServices;

namespace Nap_Reports.Sites.Controllers
{
    [Route("DailyMixParlayWinLoss_2hLink")]
    public class DailyMixParlay2hLinkController(BalanceUpService balanceUpService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var balanceUp = balanceUpService.Query();

            ViewBag.MoveBetDate = balanceUp.MoveBetsDate;
            return View();
        }

    }
}
