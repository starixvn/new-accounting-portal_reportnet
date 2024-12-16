using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class SBPerformanceMonitorController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.SBPerformanceMonitor, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        return View("~/wwwroot/sb-performance/index.cshtml");
    }
}