using SWAG.TestTask.Controllers;
using SWAG.TestTask.Infrastructure;
using NUnit.Framework;

namespace SWAG.TestTaskTests
{
    class MathControllerTest
    {
        const int value1 = 6;
        const int value2 = 3;

        [Test, Order(1)]
        public void AddTest()
        {
            var controller = new MathController(new MathDbRepository());
            var id = controller.Add(value1, value2);
            var result = controller.GetResultById((int)id);
            Assert.AreEqual(result, 9);
        }
        [Test, Order(2)]
        public void SubstractTest()
        {
            var controller = new MathController(new MathDbRepository());
            var id = controller.Substract(value1, value2);
            var result = controller.GetResultById((int)id);
            Assert.AreEqual(result, 3);
        }
        [Test, Order(3)]
        public void MultiplyTest()
        {
            var controller = new MathController(new MathDbRepository());
            var id = controller.Multiply(value1, value2);
            var result = controller.GetResultById((int)id);
            Assert.AreEqual(result, 18);
        }
        [Test, Order(4)]
        public void DivideTest()
        {
            var controller = new MathController(new MathDbRepository());
            var id = controller.Divide(value1, value2);
            var result = controller.GetResultById((int)id);
            Assert.AreEqual(result, 2);
        }
        [Test, Order(4)]
        public void DivideByZeroTest()
        {
            var controller = new MathController(new MathDbRepository());
            var id = controller.Divide(value1, 0);
 
            Assert.AreEqual(id, null);
        }
        [Test, Order(5)]
        public void PowTest()
        {
            var controller = new MathController(new MathDbRepository());
            var id = controller.Pow(value1, value2);
            var result = controller.GetResultById((int)id);
            Assert.AreEqual(result, 216);
        }
    }
}
