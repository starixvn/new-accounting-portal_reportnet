using Microsoft.AspNetCore.Mvc;

namespace Reports.Sites.Controllers;

public class TotalSMAMemberActiveController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}