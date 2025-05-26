using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using dotnet_file_exporter.Models;
using dotnet_file_exporter.Repository;
using Microsoft.Extensions.Configuration;

namespace dotnet_file_exporter.Data
{


    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("GetEmployees", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Fullname = reader.GetString(reader.GetOrdinal("Fullname")),
                            Position = reader.GetString(reader.GetOrdinal("Position")),
                            Address = reader.GetString(reader.GetOrdinal("Address"))
                        });
                    }
                }
            }

            return employees;
        }
    }
}
