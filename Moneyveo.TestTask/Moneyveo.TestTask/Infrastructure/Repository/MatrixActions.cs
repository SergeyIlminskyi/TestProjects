using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moneyveo.TestTask
{
    public class MatrixActions : IMatrixActions
    {
        public void GenerateMatrix(int size, IMatrixModel matrix)
        {
            matrix.Body = new int[size][];

            System.Random random = new System.Random();

            for(int i = 0; i < size; ++i)
            {
                matrix.Body[i] = new int[size];
                for (int j = 0; j < size; ++j)
                    matrix.Body[i][j] = random.Next(10, 50);
            }
                
        }

        public void RotateMatrixRight(IMatrixModel matrix)
        {
            for (int i = 0; i < matrix.Size / 2; i++)
                for (int j = 0; j < (matrix.Size - 1 - i); j++)
                {
                    if (i <= j)
                    {
                        int temp = matrix.Body[i][j];

                        matrix.Body[i][j] = matrix.Body[matrix.Size - 1 - j][i];
                        matrix.Body[matrix.Size - 1 - j][i] = matrix.Body[matrix.Size - 1 - i]
                                                                         [matrix.Size - 1 - j];

                        matrix.Body[matrix.Size - 1 - i][
                                    matrix.Size - 1 - j] = matrix.Body[j][matrix.Size - 1 - i];

                        matrix.Body[j][matrix.Size - 1 - i] = temp;
                    }
                }
        }

        public void RotateMatrixLeft(IMatrixModel matrix)
        {
            for (int i = 0; i < matrix.Size / 2; i++)
                for (int j = 0; j < (matrix.Size - 1 - i); j++)
                {
                    if (i <= j)
                    {
                        int temp = matrix.Body[i][j];

                        matrix.Body[i][j] = matrix.Body[j][matrix.Size - 1 - i];
                        matrix.Body[j][matrix.Size - 1 - i] = matrix.Body[matrix.Size - 1 - i][
                                                                          matrix.Size - 1 - j];
                        matrix.Body[matrix.Size - 1 - i][
                                    matrix.Size - 1 - j] = matrix.Body[matrix.Size - 1 - j][i];
                        matrix.Body[matrix.Size - 1 - j][i] = temp;
                    }
                    
                }
        }
    }
}