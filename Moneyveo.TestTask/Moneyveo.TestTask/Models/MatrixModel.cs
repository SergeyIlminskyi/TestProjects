using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moneyveo.TestTask
{
    public class MatrixModel
    {
       public int[,] Body { get; set; }

       public int Size { get; set; }

       public bool IsEmpty { get { return Body == null; } }
    }
}