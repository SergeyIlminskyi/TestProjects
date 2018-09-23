using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMCloud.Entites
{
    public class EmployeesVacationInfo
    {
        public long Id { get; set; }

        public EmployeeInfo Vacationist { get; set; }

        public EmployeeInfo Substitutional { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
