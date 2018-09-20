using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMCloud.Structs.CollaboratorsData
{
    public class CollaboratorsVacationInfo
    {
        public long Id { get; set; }

        public CollaboratorInfo Vacationist { get; set; }

        public CollaboratorInfo Substituting { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
