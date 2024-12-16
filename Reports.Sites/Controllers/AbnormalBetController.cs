using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class AbnormalBetController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.AbnormalBetReport, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View("~/wwwroot/ab-normal-bet/index.cshtml");
    }
}