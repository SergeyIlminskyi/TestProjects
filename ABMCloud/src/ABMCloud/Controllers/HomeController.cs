using System.Web;
using System.Web.Mvc;
using ABMCloud.Models;
using ABMCloud.Helpers;
using ABMCloud.Dao;

namespace ABMCloud
{
    public class HomeController : Controller
    {
        private const string _filterCode = "CollaboratorFilter";
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeesList(EmployeeFilterModel filter)
        {
            filter = ProcessFilter(filter);

            var сollaboratorsЬodel = new EmployeeModel();//репо

            сollaboratorsЬodel.EmployeesList = _repository.GetEmployees();

            filter.CurrentPagingInfo.TotalItems = сollaboratorsЬodel.EmployeesCount;

            return View(new EmployeeViewModel()
            {
                Filter = filter,
                EmployeesModel = сollaboratorsЬodel
            });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EmployeeDetails(int? id)
        {
            return View(new EmployeeDetailsModel());
        }

        private EmployeeFilterModel ProcessFilter(EmployeeFilterModel filter) //в базовый
        {
            if (filter.FilterAction != null)
            {
                switch (filter.FilterAction)
                {
                    case "applyFilter":
                        {
                            var cFilter = new EmployeeFilterModel();
                            filter.CurrentPagingInfo.Page = 1;
                            cFilter.CopyFrom(filter);
                            Session[_filterCode] = cFilter;
                            break;
                        }
                    case "applyPage":
                        {
                            var fil = new EmployeeFilterModel();
                            fil.CopyFrom(filter);
                            if (Session[_filterCode] != null)
                            {
                                fil = Session[_filterCode] as EmployeeFilterModel;
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
                    filter = HttpContextFactory.Current.Session[_filterCode] as EmployeeFilterModel;
                }
            }

            return filter;
        }
    }
}