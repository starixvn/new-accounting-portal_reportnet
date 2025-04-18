namespace Reports.Sites.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utilities.Configurations;

public class HomeController(ConfigurationService config) : Controller
{

    public IActionResult Index()
    {
        ViewBag.IsDisableMostvisitedPage = config.IsDisableMostvisitedPage;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}