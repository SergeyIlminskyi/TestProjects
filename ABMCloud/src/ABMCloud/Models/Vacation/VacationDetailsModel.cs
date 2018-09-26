using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ABMCloud.Models
{
    public class VacationDetailsModel
    {
        public int VacationistId { get; set; }

        public int SubstitutionalId { get; set; }

        public SelectList Substitutional { get; set; }

        public EmployeeSimpleModel Vacationist { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }
    }
}