using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;

namespace Nap_Reports.Sites.Controllers;

using Utilities;

public class SaveBetReportController : Controller
{
    [NapAuth(FunctionKey = AccReportFunctionKeys.SaveBet, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View();
    }
}