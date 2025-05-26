using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_file_exporter.Models;
using ClosedXML.Excel;

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

        public IActionResult ExportExcel()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Employees");

            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Fullname";
            worksheet.Cell(1, 3).Value = "Position";
            worksheet.Cell(1, 4).Value = "Address";
            worksheet.Range("A1:D1").Style.Font.Bold = true;


            // Inserting Data
            for (int i = 0; i < _employees.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = _employees[i].Id;
                worksheet.Cell(i + 2, 2).Value = _employees[i].Fullname;
                worksheet.Cell(i + 2, 3).Value = _employees[i].Position;
                worksheet.Cell(i + 2, 4).Value = _employees[i].Address;
            }

            worksheet.Columns().AdjustToContents();

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"Employees-{DateTime.Now:yyyyMMdd}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
