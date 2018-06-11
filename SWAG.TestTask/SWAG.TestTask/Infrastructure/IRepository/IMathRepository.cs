using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWAG.TestTask.Structs;

namespace SWAG.TestTask.Infrastructure
{
    public interface IMathRepository
    {
        int AddOperation(OperationInfo operation);

        int? GetOperationResult(int operationId);
    }
}
