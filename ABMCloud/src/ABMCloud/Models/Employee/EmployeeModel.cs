using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABMCloud.Entites;

namespace ABMCloud.Models
{
    public class EmployeeModel
    {
        public IReadOnlyList<EmployeeDetailsModel> EmployeesList { get; set; }

        public long EmployeesCount { get; set; } //вынести в базовый
    }
}