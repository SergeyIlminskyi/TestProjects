using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABMCloud.Models
{
    public class CollaboratorFilterModel
    {
        public CollaboratorFilterModel()
        {
            CurrentPagingInfo = new PagingInfo();
        }

        public PagingInfo CurrentPagingInfo { get; set; }
        public string FilterAction { get; set; }


        public string Name { get; set; }

        public void CopyFrom(CollaboratorFilterModel copy)
        {
            Name = copy.Name;

            CurrentPagingInfo.Page = copy.CurrentPagingInfo.Page;
            CurrentPagingInfo.ItemsPerPage = copy.CurrentPagingInfo.ItemsPerPage;
            CurrentPagingInfo.TotalItems = copy.CurrentPagingInfo.TotalItems;
        }
    }
}