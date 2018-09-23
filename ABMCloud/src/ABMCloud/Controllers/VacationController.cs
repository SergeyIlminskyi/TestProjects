using System.Web;
using System.Web.Mvc;
using ABMCloud.Models;
using ABMCloud.Helpers;
using ABMCloud.Dao;


namespace ABMCloud.Controllers
{
    public class VacationController : BaseController
    {
        private readonly IRepository _repository;

        public VacationController(IRepository repository)
        {
            _repository = repository;
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