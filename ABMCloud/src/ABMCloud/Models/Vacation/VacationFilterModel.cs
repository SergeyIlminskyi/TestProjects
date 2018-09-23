using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABMCloud.Models
{
    public class VacationFilterModel : FilterModel
    {
        public override void CopyFrom(FilterModel copy)
        {
            base.CopyBase(copy);
        }
    }
}