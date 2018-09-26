using System.Collections.Generic;
using ABMCloud.Entities;

namespace ABMCloud.Models
{
    public class VacationModel
    {
        public IReadOnlyList<VacationInfo> VacationsList { get; set; }

        public long VacationsCount { get; set; }
    }
}