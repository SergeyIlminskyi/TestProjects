using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyveo.TestTask
{
    public interface IMatrixActions
    {
        void GenerateMatrix(int size, ref MatrixModel matrix);
    }
}
