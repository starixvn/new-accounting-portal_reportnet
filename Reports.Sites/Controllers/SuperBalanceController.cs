using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class SuperBalanceController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.SuperBalance, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View();
    }
}