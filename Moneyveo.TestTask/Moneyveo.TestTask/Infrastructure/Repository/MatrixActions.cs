using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moneyveo.TestTask
{
    public class MatrixActions : IMatrixActions
    {
        public void GenerateMatrix(int size, ref MatrixModel matrix)
        {
            matrix.Body = new int[size, size];
            matrix.Size = size;

            System.Random random = new System.Random();

            for(int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    matrix.Body[i, j] = random.Next(10,50);
        }
    }
}