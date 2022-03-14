using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPractice
{
    public interface IDepartmentsRepository
    {
        IEnumerable<Departments> GetAllDepartments();
        void InsertDepartment(string newDepName);
    }
}
