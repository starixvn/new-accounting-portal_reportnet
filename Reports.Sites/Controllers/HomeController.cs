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

    public IActionResult GetFile(string filepath)
    {
        // Returns a file from the server
        if (string.IsNullOrEmpty(filepath))
        {
            return BadRequest("File path cannot be null or empty.");
        }
        var filePath = Path.Combine(config.CurrentDirectory, filepath);
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound($"File not found: {filePath}");
        }

        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        var fileName = Path.GetFileName(filePath);
        return File(fileBytes, "application/octet-stream", fileName);
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