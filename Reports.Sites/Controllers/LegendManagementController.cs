using Microsoft.AspNetCore.Mvc;

namespace Reports.Sites.Controllers;

public class LegendManagementController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}