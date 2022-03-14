using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace DapperPractice
{
    public class DapperDepartmentRepository : IDepartmentsRepository
    {
        private readonly IDbConnection _connection;  //Field or local variable for making queries to the DB
        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Departments> GetAllDepartments()
        {
            var deps = _connection.Query<Departments>("SELECT * FROM departments");
            return deps;
        }

        public void InsertDepartment(string newDepName)
        {
            _connection.Execute("INSERT INTO departments (Name) VALUES (@departmentName);",
                new { departmentName = newDepName});
        }
    }
}
