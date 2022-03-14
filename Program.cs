using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace DapperPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion
            
            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Curent Departments: ");
            var depos = repo.GetAllDepartments();

            foreach (var dept in depos)
            {
                Console.WriteLine($"Dep ID: {dept.DepartmentID}  Name: {dept.Name}");
            }
        }
    }
}
