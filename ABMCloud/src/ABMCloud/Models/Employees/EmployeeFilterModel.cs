using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABMCloud.Models
{
    public class EmployeeFilterModel : FilterModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }


        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }


        public override void CopyFrom(FilterModel copy)
        {
            Name = ((EmployeeFilterModel)copy).Name;
            Surname = ((EmployeeFilterModel)copy).Surname;
            Patronymic = ((EmployeeFilterModel)copy).Patronymic;

            FromDate = ((EmployeeFilterModel)copy).FromDate;
            ToDate = ((EmployeeFilterModel)copy).ToDate;

            base.CopyBase(copy);
        }
    }
}