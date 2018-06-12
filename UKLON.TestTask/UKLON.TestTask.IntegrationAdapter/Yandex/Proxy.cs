using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKLON.TestTask.Structs;


namespace UKLON.TestTask.IntegrationAdapter.Yandex
{
    public class Proxy : RestProxyBase, IProxy
    {
        public RegionTrafficInfo GetRegionTrafficInfo(int regionId)
        {
            if (regionId <= 0)
                throw new ArgumentException("regionId");

            FullRegionInfo regionInfo;
            var requestUri = string.Format("reginfo.xml?region={0}", regionId);
            
            Invoke(requestUri, "", out regionInfo);

            return new RegionTrafficInfo();
        }
    }
}
