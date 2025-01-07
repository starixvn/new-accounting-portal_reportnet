using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class ResultAnalysisController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.ResultAnalysis, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View("~/wwwroot/result-analysis/index.cshtml");
    }
}