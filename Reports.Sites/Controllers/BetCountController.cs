using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;

namespace Reports.Sites.Controllers;

using Utilities;

public class BetCountController : Controller
{
    [NapAuth(FunctionKey = AccReportFunctionKeys.BetCount, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View();
    }
}