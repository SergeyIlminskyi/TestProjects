using System;
using System.Web.Http;
using SWAG.TestTask.Infrastructure;
using SWAG.TestTask.Structs;

namespace SWAG.TestTask.Controllers
{
    public class MathController : ApiController
    {
        private readonly IMathRepository _mathRepository;

        public MathController(IMathRepository mathRepository)
        {
            _mathRepository = mathRepository;
        }

        [HttpGet]
        public int? Add(int value1, int value2)
        {

            var result = value1 + value2;

            var resultId = _mathRepository.AddOperation(
                new OperationInfo()
                {
                    Value1 = value1,
                    Value2 = value2,
                    Result = result,
                    Type = OperationType.Add,
                    Date = DateTime.Now
                });

            return resultId;
        }

        [HttpGet]
        public int? Substract(int value1, int value2)
        {
            var result = value1 - value2;
            var resultId = _mathRepository.AddOperation(
                new OperationInfo()
                {
                    Value1 = value1,
                    Value2 = value2,
                    Result = result,
                    Type = OperationType.Substract,
                    Date = DateTime.Now
                });

            return resultId;
        }

        [HttpGet]
        public int? Multiply(int value1, int value2)
        {

            var result = value1 * value2;

            var resultId = _mathRepository.AddOperation(
                new OperationInfo()
                {
                    Value1 = value1,
                    Value2 = value2,
                    Result = result,
                    Type = OperationType.Multiply,
                    Date = DateTime.Now
                });

            return resultId;
        }

        [HttpGet]
        public int? Divide(int value1, int value2)
        {
            if (value2 == 0)
                return null;

            var result = value1 / value2;
            var resultId = _mathRepository.AddOperation(
                new OperationInfo()
                {
                    Value1 = value1,
                    Value2 = value2,
                    Result = result,
                    Type = OperationType.Divide,
                    Date = DateTime.Now
                });

            return resultId;
        }

        [HttpGet]
        public int? Pow(int value1, int value2)
        {
            var result = Convert.ToInt32(Math.Pow(Convert.ToDouble(value1), Convert.ToDouble(value2)));

            var resultId = _mathRepository.AddOperation(
                new OperationInfo()
                {
                    Value1 = value1,
                    Value2 = value2,
                    Result = result,
                    Type = OperationType.Pow,
                    Date = DateTime.Now
                });

            return resultId; 
        }

        [HttpGet]
        public int? GetResultById(int id)
        {
            return _mathRepository.GetOperationResult(id);
        }
    }
}
