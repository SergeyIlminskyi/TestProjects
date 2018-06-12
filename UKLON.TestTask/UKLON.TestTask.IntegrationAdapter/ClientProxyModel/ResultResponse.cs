using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKLON.TestTask.IntegrationAdapter
{
    public class ResultResponse : IResponse
    {
        public string ExternalCode { get; set; }

        public string ExternalText { get; set; }

        public Result ExecutionResult { get; set; }

        string IResponse.ResponseCode  { get { return ExternalCode; } }

        string IResponse.ResponseText  { get { return ExternalText; }  }

        public bool IsSuccess {  get { return ExecutionResult == Result.Success; } }
    }

    public enum Result
    {
        Success = 0,
        NotFoundError = 1,
        TimeoutError = 2,
        UnknownError = 3,
    }
}
