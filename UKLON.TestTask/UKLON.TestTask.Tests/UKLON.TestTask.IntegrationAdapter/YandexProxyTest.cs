using System;
using System.Collections.Generic;
using System.Linq;
using UKLON.TestTask.IntegrationAdapter.Yandex;
using NUnit.Framework;

namespace UKLON.TestTask.Tests.UKLON.TestTask.IntegrationAdapter
{
    class YandexProxyTest
    {
        const int MoscowRegionId = 213;

        [Test, Order(1)]
        public void AddTest()
        {
            var proxy = new Proxy();
            var regionInfo = proxy.GetRegionTrafficInfo(MoscowRegionId);
            Assert.AreEqual(regionInfo.Id, MoscowRegionId);
        }
    }
}
