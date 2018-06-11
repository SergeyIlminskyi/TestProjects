using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWAG.TestTask.Structs
{
    public class OperationInfo
    {
        public int Id { get; set; }

        public int Value1 { get; set; }

        public int Value2 { get; set; }

        public int Result { get; set; }

        public OperationType Type { get; set; }

        public DateTime Date { get; set; }
    }

    public enum OperationType
    {
        Add,
        Substract,
        Multiply,
        Divide,
        Pow
    }
}