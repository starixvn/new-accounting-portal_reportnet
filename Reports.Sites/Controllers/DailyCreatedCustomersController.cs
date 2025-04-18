namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Settings;

public class DailyCreatedCustomersController : Controller
{

    [NapAuth(FunctionKey = FunctionKeys.DailyCreatedCustomers, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View();
    }
}