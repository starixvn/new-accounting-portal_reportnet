namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Settings;

public class SuperBalanceController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.SuperBalance, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View();
    }
}