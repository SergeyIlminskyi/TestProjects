using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABMCloud.Models
{
    public class EmployeeViewModel
    {
        public EmployeeModel EmployeesModel { get; set; }
        public EmployeeFilterModel Filter { get; set; }
    }
}