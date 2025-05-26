using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_file_exporter.Models;
using ClosedXML.Excel;
using dotnet_file_exporter.Repository;
using System.IO;

namespace dotnet_file_exporter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(ILogger<HomeController> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAllEmployees();
            return View(employees);
        }

        public IActionResult EmployeePDF()
        {
            var employees = _employeeRepository.GetAllEmployees();

            return new Rotativa.AspNetCore.ViewAsPdf("EmployeePDF", employees)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };
        }

        public IActionResult ExportExcel()
        {
            var employees = _employeeRepository.GetAllEmployees();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Employees");

            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Fullname";
            worksheet.Cell(1, 3).Value = "Position";
            worksheet.Cell(1, 4).Value = "Address";
            worksheet.Range("A1:D1").Style.Font.Bold = true;

            int row = 2;
            foreach (var emp in employees)
            {
                worksheet.Cell(row, 1).Value = emp.Id;
                worksheet.Cell(row, 2).Value = emp.Fullname;
                worksheet.Cell(row, 3).Value = emp.Position;
                worksheet.Cell(row, 4).Value = emp.Address;
                row++;
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
