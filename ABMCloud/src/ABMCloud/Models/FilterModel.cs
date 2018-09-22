using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABMCloud.Models
{
    abstract public class FilterModel
    {
        public FilterModel()
        {
            CurrentPagingInfo = new PagingInfo();
        }

        public PagingInfo CurrentPagingInfo { get; set; }
        public string FilterAction { get; set; }

        public abstract void CopyFrom(FilterModel copy);


        protected void CopyBase(FilterModel copy)
        {
            CurrentPagingInfo.Page = copy.CurrentPagingInfo.Page;
            CurrentPagingInfo.ItemsPerPage = copy.CurrentPagingInfo.ItemsPerPage;
            CurrentPagingInfo.TotalItems = copy.CurrentPagingInfo.TotalItems;
        }

    }
}