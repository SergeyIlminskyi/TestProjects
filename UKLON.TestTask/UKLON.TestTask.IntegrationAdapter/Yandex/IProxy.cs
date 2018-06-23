using System.Collections.Generic;
using UKLON.TestTask.Structs;

namespace UKLON.TestTask.IntegrationAdapter.Yandex
{
    public interface IProxy
    {
        RegionTrafficInfo GetRegionTrafficInfo(int regionId);

        List<RegionTrafficInfoWithStatus> GetListRegionTrafficInfo(List<RegionData> regionData);

        void WriteListRegionTrafficInfo(List<RegionData> regionData);

        void WriteListRegionTrafficInfo();
    }
}
