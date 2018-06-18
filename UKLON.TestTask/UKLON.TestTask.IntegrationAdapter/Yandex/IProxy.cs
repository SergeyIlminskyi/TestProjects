using UKLON.TestTask.Structs;

namespace UKLON.TestTask.IntegrationAdapter.Yandex
{
    public interface IProxy
    {
        RegionTrafficInfo GetRegionTrafficInfo(int regionId);
    }
}
