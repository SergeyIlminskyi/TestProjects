using System.Web;
using System.Web.Mvc;
using ABMCloud.Models;
using ABMCloud.Helpers;

namespace ABMCloud
{
    public class HomeController : Controller
    {
        private const string _filterCode = "CollaboratorFilter";

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CollaboratorList(CollaboratorFilterModel filter)
        {
            filter = ProcessFilter(filter);

            var сollaboratorsЬodel = new CollaboratorModel();//репо

            filter.CurrentPagingInfo.TotalItems = сollaboratorsЬodel.CollaboratorsCount;

            return View(new CollaboratorViewModel()
            {
                Filter = filter,
                CollaboratorsModel = сollaboratorsЬodel
            });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CollaboratorDetails(int id)
        {
            return View(new CollaboratorDetailsModel());
        }

        private CollaboratorFilterModel ProcessFilter(CollaboratorFilterModel filter) //в базовый
        {
            if (filter.FilterAction != null)
            {
                switch (filter.FilterAction)
                {
                    case "applyFilter":
                        {
                            var cFilter = new CollaboratorFilterModel();
                            filter.CurrentPagingInfo.Page = 1;
                            cFilter.CopyFrom(filter);
                            Session[_filterCode] = cFilter;
                            break;
                        }
                    case "applyPage":
                        {
                            var fil = new CollaboratorFilterModel();
                            fil.CopyFrom(filter);
                            if (Session[_filterCode] != null)
                            {
                                fil = Session[_filterCode] as CollaboratorFilterModel;
                                if (fil != null) fil.CurrentPagingInfo = filter.CurrentPagingInfo;
                            }
                            Session[_filterCode] = fil;
                            return fil;
                        }
                    case "resetFilter":
                        {
                            if (HttpContextFactory.Current.Session != null)
                                HttpContextFactory.Current.Session[_filterCode] = null;
                            break;
                        }
                }
            }
            else
            {
                if (HttpContextFactory.Current.Session != null && HttpContextFactory.Current.Session[_filterCode] != null)
                {
                    filter = HttpContextFactory.Current.Session[_filterCode] as CollaboratorFilterModel;
                }
            }

            return filter;
        }
    }
}