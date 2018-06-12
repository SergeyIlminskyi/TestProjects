using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKLON.TestTask.IntegrationAdapter.Yandex;

namespace UKLON.TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Proxy();

            var res = t.GetRegionTrafficInfo(143);
        }
    }
}
