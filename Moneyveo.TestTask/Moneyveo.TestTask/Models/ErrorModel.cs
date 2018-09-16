using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moneyveo.TestTask.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }

        public ErrorType Type { get; set; }
    }

    public enum ErrorType
    {
        Info = 0,
        Warning = 1,
        Critical = 2
    }

}