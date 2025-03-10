namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Utilities;

public class CompanyBalanceDifferenceController : Controller
{
    [NapAuth(FunctionKey = AccReportFunctionKeys.CompanyBalanceDifference, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View();
    }
}