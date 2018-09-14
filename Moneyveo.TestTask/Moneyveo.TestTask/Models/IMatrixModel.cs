using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyveo.TestTask
{
    public interface IMatrixModel
    {
        int[][] Body { get; set; }

        int Size { get; }

        bool IsEmpty { get; }
    }
}
