namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Settings;

public class MarginReportController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.MarginReport, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View();
    }
}