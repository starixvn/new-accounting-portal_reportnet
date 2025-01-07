using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class SMAWinLossController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.SMAWinLossSummary, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View("~/wwwroot/sma-win-loss/index.cshtml");
    }
}