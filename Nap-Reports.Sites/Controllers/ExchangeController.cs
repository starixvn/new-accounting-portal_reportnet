using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Sites.Settings;

namespace Nap_Reports.Sites.Controllers
{
    public class ExchangeController : Controller
    {
        [NapAuth(FunctionKey = FunctionKeys.Exchange, Policy = Constant.Policy.Read)]
        public IActionResult Index()
        {
            return View("~/wwwroot/exchange/index.cshtml");
        }
    }
}
