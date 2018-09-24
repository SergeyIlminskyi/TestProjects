using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABMCloud.Entites;

namespace ABMCloud.Dao
{
    public interface IRepository
    {
        int AddEmployee(EmployeeInfo employee);

        void EditEmployee(EmployeeInfo employee);

        List<EmployeeInfo> GetEmployees();

        EmployeeInfo GetEmployeeDetailsById(int id);

        int AddVacation(EmployeesVacationInfo vacation);

        List<EmployeesVacationInfo> GetVacationsByVacationistId(int id);


        void Test();
    }
}
