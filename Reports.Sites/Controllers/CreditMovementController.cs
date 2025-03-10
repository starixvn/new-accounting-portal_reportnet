namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Utilities;

public class CreditMovementController : Controller
{
    [NapAuth(FunctionKey = AccReportFunctionKeys.CreditMovement, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        ViewBag.Amount = "300,000";
        return View();
    }
}