using Microsoft.AspNetCore.Mvc;
using Utilities.BalanceUpServices;

namespace Nap_Reports.Sites.Controllers
{
    public class TreeWinLossController : Controller
    {
        private readonly BalanceUpService balanceUpService;

        public TreeWinLossController(BalanceUpService balanceUpService)
        {
            this.balanceUpService = balanceUpService;
        }

        public IActionResult Index()
        {
            var balanceUp = balanceUpService.Query();

            ViewBag.MoveBetDate = balanceUp.MoveBetsDate;
            return View();
        }
    }
}
