using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace ABMCloud.Models
{
    public class PagingInfo //сделать базовым
    {
        public PagingInfo()
        {
            Page = 1;
            ItemsPerPage = 10;
        }

        public int ItemsPerPage { get; set; }

        public int Page { get; set; }

        public long TotalItems { get; set; }

        public RouteValueDictionary RouteValueDictionary { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}