using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMCloud.Dao.Entitis
{
    class EmployeesVacation : Entity
    {
        public int Id { get; set; }

        public Employee Vacationist { get; set; }

        public Employee Substitutional { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
