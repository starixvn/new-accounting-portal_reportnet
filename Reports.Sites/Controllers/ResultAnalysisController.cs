namespace Reports.Sites.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Settings;

public class ResultAnalysisController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.ResultAnalysis, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        return View("~/wwwroot/result-analysis/index.cshtml");
    }
}