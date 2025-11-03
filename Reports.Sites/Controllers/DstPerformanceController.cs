using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class DstPerformanceController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.DSTPerformance, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View("~/wwwroot/dst-performance/index.cshtml");
    }
}