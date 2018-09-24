using System;

namespace ABMCloud.Entities
{
    public class VacationInfo
    {
        public long Id { get; set; }

        public EmployeeInfo Vacationist { get; set; }

        public EmployeeInfo Substitutional { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
