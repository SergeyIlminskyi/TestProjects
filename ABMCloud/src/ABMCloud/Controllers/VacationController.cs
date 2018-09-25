using System.Web;
using System.Web.Mvc;
using ABMCloud.Models;
using ABMCloud.Helpers;
using ABMCloud.Dao;


namespace ABMCloud
{ 
    public class VacationController : BaseController
    {
        private readonly IRepository _repository;

        public VacationController(IRepository repository)
        {
            _repository = repository;
        }
    }
}