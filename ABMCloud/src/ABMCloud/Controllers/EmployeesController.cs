using System.Collections.Generic;
using System.Web.Mvc;
using ABMCloud.Models;
using ABMCloud.Helpers;
using ABMCloud.Dao;
using AutoMapper;

namespace ABMCloud
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

            var сollaboratorsЬodel = new EmployeeModel()
            {
                EmployeesList = Mapper.Map<List<EmployeeDetailsModel>>(_repository.GetEmployees())
            };

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
            var model = Mapper.Map<EmployeeDetailsModel>(_repository.GetEmployeeDetailsById(id));
            model.VacationFilter = new VacationFilterModel();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddOrEditEmployee(int? id)
        {
            if(id.HasValue)
                return View(Mapper.Map<EmployeeDetailsModel>(_repository.GetEmployeeDetailsById((int)id)));
            else
                return View(new EmployeeDetailsModel());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddOrEditEmployee(EmployeeDetailsModel model)
        {
            base.ShowSuccessMessage = true;

            if (model.Id > 0)
                _repository.EditEmployee(Mapper.Map<Entities.EmployeeInfo>(model));
            else
                _repository.AddEmployee(Mapper.Map<Entities.EmployeeInfo>(model));

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

            var сollaboratorsЬodel = new VacationModel()
            {
                VacationsList = _repository.GetVacationsByVacationistId(model.Id, Mapper.Map<Entities.VacationFilter>(filter)),
            };

            filter.CurrentPagingInfo.TotalItems = 50;

            return PartialView(new VacationViewModel()
            {
                Filter = filter,
                VacationsModel = сollaboratorsЬodel
            });
        }
    }
}