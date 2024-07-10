using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Sites.Settings;

namespace Nap_Reports.Sites.Controllers
{
    public class ResultAnalysisController : Controller
    {
        [NapAuth(FunctionKey = FunctionKeys.ResultAnalysis, Policy = Constant.Policy.Read)]
        public IActionResult Index()
        {
            return View("~/wwwroot/result-analysis/index.cshtml");
        }
    }
}
