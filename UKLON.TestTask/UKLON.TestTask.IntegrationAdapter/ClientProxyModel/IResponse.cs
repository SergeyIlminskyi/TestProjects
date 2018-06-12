using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKLON.TestTask.IntegrationAdapter
{
    interface IResponse
    {
        string ResponseCode { get; }
        string ResponseText { get; }
    }
}
