using System;

namespace ABMCloud.Models
{
    public class EmployeeFilterModel : FilterModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }


        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }


        public override void CopyFrom(FilterModel copy)
        {
            Name = ((EmployeeFilterModel)copy).Name;
            Surname = ((EmployeeFilterModel)copy).Surname;
            Patronymic = ((EmployeeFilterModel)copy).Patronymic;

            DateFrom = ((EmployeeFilterModel)copy).DateFrom;
            DateTo = ((EmployeeFilterModel)copy).DateTo;

            base.CopyBase(copy);
        }
    }
}