using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moneyveo.TestTask
{
    public class MatrixModel : IMatrixModel
    {
       public MatrixModel()
       {
       }
       public int[][] Body { get; set; }

       public int Size { get { return this.Body?.GetLength(0) ?? 0; } }

       public bool IsEmpty { get { return Body == null; } }
    }
}