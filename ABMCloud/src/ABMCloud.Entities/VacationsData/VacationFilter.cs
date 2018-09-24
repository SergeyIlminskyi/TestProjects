using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMCloud.Entities
{
    public class VacationFilter
    {
        public DateTime? StartDateFrom { get; set; }

        public DateTime? StartDateTo { get; set; }

        public DateTime? EndDateFrom { get; set; }

        public DateTime? EndDateTo { get; set; }
    }
}
