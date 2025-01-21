using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class SabaPromotionController: Controller
{
    [NapAuth(FunctionKey = FunctionKeys.SabaPromotion, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        return View("~/wwwroot/saba-promotion/index.cshtml");
    }
}