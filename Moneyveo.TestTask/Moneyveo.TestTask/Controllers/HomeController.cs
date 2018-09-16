using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moneyveo.TestTask.Helpers;

namespace Moneyveo.TestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly string matrixViewPath = "../Shared/HomePartialView/Matrix";

        private IMatrixActions _matrixActions;
        private IMatrixModel _matrix;

        public HomeController(IMatrixActions matrixActions, IMatrixModel matrix)
        {
            _matrixActions = matrixActions;
            _matrix = matrix;
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ImportMatrixFromFile(HttpPostedFileBase file)
        {
            _matrix.Body = CSVParser.ImportCSV(file, ';');
            return PartialView(matrixViewPath, _matrix);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GenerateMatrix(int size)
        {
            if (size <= 1)
                throw new ValidationException("Size of matrix must be 2x2 and more.");

            _matrixActions.GenerateMatrix(size, _matrix);
            return PartialView(matrixViewPath, _matrix);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RotateMatrixRight()
        {
            _matrixActions.RotateMatrixRight(_matrix);
            return PartialView(matrixViewPath, _matrix);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RotateMatrixLeft()
        {
            _matrixActions.RotateMatrixLeft(_matrix);
            return PartialView(matrixViewPath, _matrix);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public FileContentResult ExportMatrixToFile()
        {
            using (var memoryStream = new System.IO.MemoryStream())
            using (var streamWriter = new System.IO.StreamWriter(memoryStream))
            {
                CSVParser.ExportCSV( _matrix, streamWriter, ';');
                streamWriter.Flush();
                return File(memoryStream.ToArray(), "text/csv", "ExportMatrix.csv");
            }
        }
    }
}