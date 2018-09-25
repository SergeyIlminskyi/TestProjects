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
            long totalItems;
            var сollaboratorsЬodel = new EmployeeModel()
            {
                EmployeesList = Mapper.Map<List<EmployeeDetailsModel>>(_repository.GetEmployees(Mapper.Map<Entities.EmployeeFilter>(filter),out totalItems))
            };

            filter.CurrentPagingInfo.TotalItems = totalItems;

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
            if (ModelState.IsValid)
            {
                base.ShowSuccessMessage = true;

                if (model.Id > 0)
                    _repository.EditEmployee(Mapper.Map<Entities.EmployeeInfo>(model));
                else
                    _repository.AddEmployee(Mapper.Map<Entities.EmployeeInfo>(model));

                return View(model);
            }

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RemoveEmployee(int id)
        {
            return View(new EmployeeDetailsModel());
        }

        public ActionResult VacationsByEmploee(EmployeeDetailsModel model, VacationFilterModel filter)
        {
            filter = ProcessFilter<VacationFilterModel>(filter);

            long totalItems;
            var сollaboratorsЬodel = new VacationModel()
            {
                VacationsList = _repository.GetVacationsByVacationistId(model.Id, out totalItems, Mapper.Map<Entities.VacationFilter>(filter)),
            };
            filter.CurrentPagingInfo.TotalItems = totalItems;

            return PartialView(new VacationViewModel()
            {
                Filter = filter,
                VacationsModel = сollaboratorsЬodel
            });
        }
    }
}