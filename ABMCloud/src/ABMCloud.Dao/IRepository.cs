using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABMCloud.Entities;

namespace ABMCloud.Dao
{
    public interface IRepository
    {
        int AddEmployee(EmployeeInfo employee);

        void EditEmployee(EmployeeInfo employee);

        List<EmployeeInfo> GetEmployees(EmployeeFilter filter = null);

        EmployeeInfo GetEmployeeDetailsById(int id);

        int AddVacation(VacationInfo vacation);

        List<VacationInfo> GetVacationsByVacationistId(int id, VacationFilter filter = null);


        void Test();
    }
}
