using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace ABMCloud.Models
{
    public class VacationDetailsModel
    {
        public int VacationistId { get; set; }

        public int SubstitutionalId { get; set; }

        public SelectList Substitutional { get; set; }

        public EmployeeSimpleModel Vacationist { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}