using dotnet_file_exporter.Models;
using System.Collections.Generic;

namespace dotnet_file_exporter.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
    }
}
