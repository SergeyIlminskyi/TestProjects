using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABMCloud.Models
{
    public class VacationViewModel
    {
        public VacationModel VacationsModel { get; set; }
        public VacationFilterModel Filter { get; set; }
    }
}