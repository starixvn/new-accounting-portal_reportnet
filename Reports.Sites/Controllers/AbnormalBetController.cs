namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Settings;

public class AbnormalBetController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.AbnormalBetReport, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View("~/wwwroot/ab-normal-bet/index.cshtml");
    }
}