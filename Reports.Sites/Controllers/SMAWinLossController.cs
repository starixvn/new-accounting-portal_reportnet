namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Settings;

public class SMAWinLossController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.SMAWinLossSummary, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View("~/wwwroot/sma-win-loss/index.cshtml");
    }
}