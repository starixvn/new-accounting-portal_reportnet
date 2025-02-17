using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites.Settings;

namespace Reports.Sites.Controllers;

public class SoccerPerformanceDetailController : Controller
{
    public IActionResult BetTypePerformance()
    {
        return View();
    }
}