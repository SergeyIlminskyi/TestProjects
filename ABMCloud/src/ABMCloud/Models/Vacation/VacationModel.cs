using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABMCloud.Entites;

namespace ABMCloud.Models
{
    public class VacationModel
    {
        public IReadOnlyList<EmployeesVacationInfo> VacationsList { get; set; }

        public long VacationsCount { get; set; } //вынести в базовый
    }
}