namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Utilities;
using Utilities.BalanceUpServices;

public class TraderPerformanceSoccerController(BalanceUpService balanceUpService) : Controller
{
    [NapAuth(FunctionKey = AccReportFunctionKeys.TraderPerformanceSoccer, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        var balanceUp = balanceUpService.Query();
        ViewBag.MinDate = balanceUp.MoveBetsDate.AddDays(1).ToShortDateString();
        return View();
    }
}