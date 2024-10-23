using Microsoft.AspNetCore.Mvc;

namespace Nap_Reports.Sites.Controllers;

public class LegendManagementController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}