namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Utilities;

public class DirectBalanceController : Controller
{
    [NapAuth(FunctionKey = AccReportFunctionKeys.DirectBalance, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View();
    }
}