using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Utilities.BalanceUpServices;

namespace Reports.Sites.Controllers;

using Utilities;

public class TraderPerformanceOtherSportsController(BalanceUpService balanceUpService) : Controller
{
    [NapAuth(FunctionKey = AccReportFunctionKeys.TraderPerformanceOtherSports, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        var balanceUp = balanceUpService.Query();
        ViewBag.MinDate = balanceUp.MoveBetsDate.AddDays(1).ToShortDateString();

        return View();
    }
}