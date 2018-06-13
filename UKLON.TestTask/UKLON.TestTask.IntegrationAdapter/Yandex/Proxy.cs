﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKLON.TestTask.Structs;


namespace UKLON.TestTask.IntegrationAdapter.Yandex
{
    public class Proxy : HttpProxyBase, IProxy
    {
        public RegionTrafficInfo GetRegionTrafficInfo(int regionId)
        {
            if (regionId <= 0)
                throw new ArgumentException("regionId");

            ResultResponse result;
            var requestUri = string.Format("reginfo.xml?region={0}", regionId); //За неимением документации лучше не придумал 
            
            var t = Invoke<FullRegionInfo>(requestUri, out result);

            return new RegionTrafficInfo();
        }
    }
}
