namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Settings;

public class SBPerformanceMonitorController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.SBPerformanceMonitor, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        return View("~/wwwroot/sb-performance/index.cshtml");
    }
}