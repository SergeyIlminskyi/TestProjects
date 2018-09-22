using System.Web;
using System.Web.Mvc;
using ABMCloud.Models;
using ABMCloud.Helpers;
using ABMCloud.Dao;

namespace ABMCloud.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IRepository _repository;

        public EmployeeController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult EmployeesList(EmployeeFilterModel filter)
        {
            filter = ProcessFilter<EmployeeFilterModel>(filter);

            var сollaboratorsЬodel = new EmployeeModel();//репо
            сollaboratorsЬodel.EmployeesList = _repository.GetEmployees();

            filter.CurrentPagingInfo.TotalItems = 50;

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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditEmployee(int? id)
        {
            return View(new EmployeeDetailsModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RemoveEmployee(int? id)
        {
            return View(new EmployeeDetailsModel());
        }
    }
}