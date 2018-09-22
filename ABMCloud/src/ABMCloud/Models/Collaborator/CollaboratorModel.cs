using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABMCloud.Entites;

namespace ABMCloud.Models
{
    public class CollaboratorModel
    {
        public IReadOnlyList<EmployeeInfo> LogsList { get; set; }

        public long CollaboratorsCount { get; set; } //вынести в базовый
    }
}