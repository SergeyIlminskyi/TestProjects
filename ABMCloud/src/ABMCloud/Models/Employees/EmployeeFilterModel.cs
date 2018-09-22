using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABMCloud.Models
{
    public class EmployeeFilterModel : FilterModel
    {
        public string Name { get; set; }

        public override void CopyFrom(EmployeeFilterModel copy)
        {
            Name = copy.Name;

            CurrentPagingInfo.Page = copy.CurrentPagingInfo.Page;
            CurrentPagingInfo.ItemsPerPage = copy.CurrentPagingInfo.ItemsPerPage;
            CurrentPagingInfo.TotalItems = copy.CurrentPagingInfo.TotalItems;
        }
    }
}