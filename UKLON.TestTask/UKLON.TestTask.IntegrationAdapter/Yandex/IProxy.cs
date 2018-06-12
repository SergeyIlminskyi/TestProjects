using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKLON.TestTask.Structs;

namespace UKLON.TestTask.IntegrationAdapter.Yandex
{
    public interface IProxy
    {
        RegionTrafficInfo GetRegionTrafficInfo(int regionId);
    }
}
