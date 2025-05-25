using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_file_exporter.Models;

namespace dotnet_file_exporter.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var employees = new[]
         {
        new { id = 1, fullname = "John Doe", position = "Manager", address = "123 Main St" },
        new { id = 2, fullname = "Jane Smith", position = "Developer", address = "456 Elm St" },
        new { id = 3, fullname = "Alice Johnson", position = "Designer", address = "789 Oak St" }
        };

        ViewData["employees"] = employees;
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
