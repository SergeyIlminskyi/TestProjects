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
        public ActionResult EmployeeDetails(int id)
        {
            return View(new EmployeeDetailsModel()
            {
                EmployeeDetails = _repository.GetEmployeeDetailsById((int)id)
            });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddOrEditEmployee(int? id)
        {
            return View(new EmployeeDetailsModel()
            {
                EmployeeDetails = _repository.GetEmployeeDetailsById((int)id)
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddOrEditEmployee(EmployeeDetailsModel model)
        {
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RemoveEmployee(int id)
        {
            return View(new EmployeeDetailsModel());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VacationsByEmploee(EmployeeDetailsModel model, VacationFilterModel filter)
        {
            filter = ProcessFilter<VacationFilterModel>(filter);

            var сollaboratorsЬodel = new VacationModel();//репо
            сollaboratorsЬodel.VacationsList = _repository.GetVacationsByVacationistId(model.EmployeeDetails.Id);

            filter.CurrentPagingInfo.TotalItems = 50;

            return PartialView(new VacationViewModel()
            {
                Filter = filter,
                VacationsModel = сollaboratorsЬodel
            });
        }
    }
}