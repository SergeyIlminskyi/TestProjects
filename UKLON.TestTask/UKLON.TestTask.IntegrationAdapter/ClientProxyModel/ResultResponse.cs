using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKLON.TestTask.IntegrationAdapter
{
    public class ResultResponse : IResponse
    {
        private Result responseResult { get; set; }

        private string responseText { get; set; }

        Result IResponse.ResponseCode  { get { return responseResult; } }

        string IResponse.ResponseText  { get { return responseText; }  }

        public bool IsSuccess {  get { return responseResult == Result.Success; } }

        public ResultResponse(Result executionResult, Dictionary<Result, string> customMapping)
        {
            string text;
            responseResult = executionResult;

            if (customMapping.TryGetValue(executionResult, out text))
                responseText = text;
            else
                responseText = "";
        }
    }

    public enum Result
    {
        Success = 0,
        TimeoutError = 1,
        UnknownError = 2,
    }
}
