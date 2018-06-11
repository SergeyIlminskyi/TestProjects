using SWAG.TestTask.Structs;
using System.Collections.Generic;
using System.Linq;

namespace SWAG.TestTask.Infrastructure
{
    public class MathDbRepository : IMathRepository
    {
        private int counter = 0;
        private List<OperationInfo> operations = new List<OperationInfo>();

        public int AddOperation(OperationInfo operation)
        {
            counter++;
            operation.Id = counter;
            
            operations.Add(operation);

            return counter;
        }
        public int? GetOperationResult(int operationId)
        {
            return operations.Where(x => x.Id == operationId).FirstOrDefault().Result;
        }
    }
}