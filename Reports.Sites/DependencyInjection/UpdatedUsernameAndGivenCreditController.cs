using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Sites.Settings;

namespace Nap_Reports.Sites.DependencyInjection
{
    public class UpdatedUsernameAndGivenCreditController : Controller
    {
        [NapAuth(FunctionKey = FunctionKeys.UpdatedUsernameAndGivenCredit, Policy = Constant.Policy.Read)]
        public IActionResult Index()
        {
            return View("~/wwwroot/updated-username-credit/index.cshtml");
        }
    }
}
