using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class ActualMemberBetController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.ActualMemberBet, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View("~/wwwroot/actual-member-bet/index.cshtml");
    }
}