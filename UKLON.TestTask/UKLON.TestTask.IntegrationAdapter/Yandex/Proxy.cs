using System;
using UKLON.TestTask.Structs;


namespace UKLON.TestTask.IntegrationAdapter.Yandex
{
    public class Proxy : HttpProxyBase, IProxy
    {
        protected readonly IFileWorker _fileWorker;

        public Proxy(IFileWorker fileWorker)
        {
            _fileWorker = fileWorker;
        }

        public RegionTrafficInfo GetRegionTrafficInfo(int regionId)
        {
            if (regionId <= 0)
                throw new ArgumentException("regionId");

            RegionTrafficInfo regionTrafficInfo;

            ResultResponse result;
            var requestUri = string.Format("reginfo.xml?region={0}&lang={1}", regionId, "en"); //За неимением документации лучше не придумал 
            
            var response = Invoke<FullRegionInfo>(requestUri, out result);

            if (result.IsSuccess && response?.Traffic.Region?.Id == regionId) //Эта проверка добавлена, поскольку API возвращает дефолтный регион в том случае, если регион по Id не найден
                regionTrafficInfo = (RegionTrafficInfo)response;
            else
                regionTrafficInfo = new RegionTrafficInfo();
  
            _fileWorker.WriteToFile(regionTrafficInfo, result);

            return regionTrafficInfo;
        }
    }
}
