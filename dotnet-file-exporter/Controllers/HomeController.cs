using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_file_exporter.Models;

namespace dotnet_file_exporter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly List<Employee> _employees;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _employees = new List<Employee>
                {
                    new Employee { Id = 1, Fullname = "John Doe", Position = "Manager", Address = "123 Main St" },
                    new Employee { Id = 2, Fullname = "Jane Smith", Position = "Developer", Address = "456 Elm St" },
                    new Employee { Id = 3, Fullname = "Alice Johnson", Position = "Designer", Address = "789 Oak St" }
                };
        }

        public IActionResult Index()
        {
            return View(_employees);
        }

        public IActionResult EmployeePDF()
        {
            return new Rotativa.AspNetCore.ViewAsPdf("EmployeePDF", _employees)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };
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
