namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Utilities;
using Utilities.BalanceUpServices;

public class ParlayWonController(BalanceUpService balanceUpService) : Controller
{
    [NapAuth(FunctionKey = AccReportFunctionKeys.ParlayWon, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        var balanceUp = balanceUpService.Query();
        ViewBag.BalanceUpDay = balanceUp.BalanceUpDate;
           
        return View();
    }
}