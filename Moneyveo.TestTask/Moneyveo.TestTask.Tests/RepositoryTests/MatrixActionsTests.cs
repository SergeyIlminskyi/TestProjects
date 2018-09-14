using NUnit.Framework;
using System.Linq;

namespace Moneyveo.TestTask.Tests.RepositoryTests
{
    class MatrixActionsTests
    {
        MatrixActions actions = new MatrixActions();
        MatrixModel matrix = new MatrixModel()
        {
            Body = new int[4][]
            {
                new int[4],
                new int[4],
                new int[4],
                new int[4]
            }
        };


        private readonly int[][] baseMatrix = new int[4][] 
        {   
            new int[] {21,15,58,28},
            new int[] {46,64,85,68},
            new int[] {11,78,77,41},
            new int[] {57,73,91,17}
        };

        private readonly int[][] rightMatrix = new int[4][]
        {
            new int[] {57,11,46,21},
            new int[] {73,78,64,15},
            new int[] {91,77,85,58},
            new int[] {17,41,68,28}
        };

        private readonly int[][] leftMatrix = new int[4][]
        {
            new int[] {28,68,41,17},
            new int[] {58,85,77,91},
            new int[] {15,64,78,73},
            new int[] {21,46,11,57}
        };


        [Test, Order(1)]
        public void GenerateMatrixTest()
        {
            actions.GenerateMatrix(4,matrix);
            Assert.IsNotNull(matrix.Body);
        }


        [Test, Order(2)]
        public void RotateMatrixRightTest()
        {

            matrix.Body = baseMatrix.Select(a => a.ToArray()).ToArray();
            actions.RotateMatrixRight(matrix);
            Assert.AreEqual(matrix.Body, rightMatrix);
        }


        [Test, Order(3)]
        public void RotateMatrixLeftTest()
        {
            matrix.Body = baseMatrix.Select(a => a.ToArray()).ToArray();
            actions.RotateMatrixLeft(matrix);
            Assert.AreEqual(matrix.Body, leftMatrix);
        }
    }
}
