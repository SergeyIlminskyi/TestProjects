using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moneyveo.TestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly string matrixViewPath = "../Shared/HomePartialView/Matrix";


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ImportMatrixFromFile(HttpPostedFileBase file)
        {
            return PartialView(matrixViewPath);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GenerateMatrix(int size)
        {
            return PartialView(matrixViewPath);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RotateMatrix()
        {
            return PartialView(matrixViewPath);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public FileContentResult ExportMatrixToFile()
        {
            using (var memoryStream = new System.IO.MemoryStream())
            using (var streamWriter = new System.IO.StreamWriter(memoryStream))
            {
                return File(memoryStream.ToArray(), "text/csv", "ExportMatrix.csv");
            }
        }
    }
}