namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Settings;
using Utilities.BalanceUpServices;

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