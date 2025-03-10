namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Settings;

public class BetConstructPerformanceController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.BetConstructPerformance, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        return View("~/wwwroot/bet-construct/index.cshtml");
    }
}