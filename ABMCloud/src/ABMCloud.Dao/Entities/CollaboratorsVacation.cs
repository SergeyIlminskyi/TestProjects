using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMCloud.Dao.Entitis
{
    class CollaboratorsVacation : Entity
    {
        public long Id { get; set; }

        public long VacationistId{ get; set; }

        public long SubstitutingId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
