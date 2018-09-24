using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABMCloud.Entites;

namespace ABMCloud.Models
{
    public class EmployeeDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime? LastVacationDate { get; set; }
    }
}