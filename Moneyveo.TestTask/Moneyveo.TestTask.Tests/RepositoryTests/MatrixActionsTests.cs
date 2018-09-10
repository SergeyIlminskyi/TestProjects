using NUnit.Framework;

namespace Moneyveo.TestTask.Tests.RepositoryTests
{
    class MatrixActionsTests
    {
        MatrixActions actions = new MatrixActions();
        MatrixModel matrix = new MatrixModel();

        private readonly int[,] baseMatrix = new int[,] 
        {   
            {21,15,58,28},
            {46,64,85,68},
            {11,78,77,41}, 
            {57,73,91,17}
        };

        private readonly int[,] rightMatrix = new int[,]
        {   
            {57,11,46,21},
            {73,78,64,15},
            {91,77,85,58},
            {17,41,68,28}
        };

        private readonly int[,] leftMatrix = new int[,]
        {   
            {28,68,41,17},
            {58,85,77,91},
            {15,64,78,73},
            {21,46,11,57}
        };


        [Test, Order(1)]
        public void GenerateMatrixTest()
        {
            actions.GenerateMatrix(5,matrix);
            Assert.IsNotNull(matrix.Body);
        }


        [Test, Order(2)]
        public void RotateMatrixRightTest()
        {
           
            matrix.Body = baseMatrix;
            actions.RotateMatrixRight(matrix);
            Assert.AreEqual(matrix.Body, leftMatrix);
        }


        [Test, Order(3)]
        public void RotateMatrixLeftTest()
        {
            matrix.Body = baseMatrix;
            actions.RotateMatrixLeft(matrix);
            Assert.AreEqual(matrix.Body, leftMatrix);
        }
    }
}
