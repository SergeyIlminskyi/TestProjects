using System;

namespace ABMCloud.Models
{
    public class VacationFilterModel : FilterModel
    {
        public DateTime? StartDateFrom { get; set; }

        public DateTime? StartDateTo { get; set; }

        public DateTime? EndDateFrom { get; set; }

        public DateTime? EndDateTo { get; set; }


        public override void CopyFrom(FilterModel copy)
        {
            base.CopyBase(copy);

            StartDateFrom = ((VacationFilterModel)copy).StartDateFrom;
            StartDateTo = ((VacationFilterModel)copy).StartDateTo;
            EndDateFrom = ((VacationFilterModel)copy).EndDateFrom;
            EndDateTo = ((VacationFilterModel)copy).EndDateTo;
        }
    }
}