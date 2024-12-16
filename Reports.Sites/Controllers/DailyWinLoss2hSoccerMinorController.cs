using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class DailyWinLoss2hSoccerMinorController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.DailyWinLoss2hSoccerMinorBettype, Policy = Constant.Policy.Read)]
    public IActionResult Index()
    {
        ViewBag.ProductPermissions = new List<string> { "HouseWinLossByProducts_AllProducts" };
        ViewBag.ModePermissions = new List<string> { "HouseWinLossByProducts_HistoryMode", "HouseWinLossByProducts_NormalMode" };
        return View();
    }
}