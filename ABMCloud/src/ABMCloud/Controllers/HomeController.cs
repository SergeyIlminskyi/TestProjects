using System.Web;
using System.Web.Mvc;
using ABMCloud.Models;
using ABMCloud.Helpers;
using ABMCloud.Dao;

namespace ABMCloud
{
    public class HomeController : BaseController
    {
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
            filter = ProcessFilter<EmployeeFilterModel>(filter);

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
    }
}