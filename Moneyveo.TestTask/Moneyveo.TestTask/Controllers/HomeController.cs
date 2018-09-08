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

        private IMatrixActions _matrixActions;
        private MatrixModel matrix = new MatrixModel();

        public HomeController(IMatrixActions matrixActions)
        {
            _matrixActions = matrixActions;
        }


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
            _matrixActions.GenerateMatrix(size, ref matrix);
            return PartialView(matrixViewPath, matrix);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RotateMatrixRight()
        {
            _matrixActions.GenerateMatrix(6, ref matrix);

            return PartialView(matrixViewPath, matrix);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RotateMatrixLeft()
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