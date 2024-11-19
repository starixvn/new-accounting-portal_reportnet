using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Sites.Models;
using System.Diagnostics;
using Utilities.Configurations;

namespace Nap_Reports.Sites.Controllers
{
    public class HomeController(ILogger<HomeController> logger, ConfigurationService config) : Controller
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
}
