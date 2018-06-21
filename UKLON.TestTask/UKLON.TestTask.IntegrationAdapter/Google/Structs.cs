using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKLON.TestTask.Structs;

namespace UKLON.TestTask.IntegrationAdapter.Google
{
    internal class GoogleRegionData
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public static explicit operator GoogleRegionData(RegionData regionData)
        {
            if (regionData == null)
                return null;

            return new GoogleRegionData()
            {
                Id = regionData.Id,
                Name = regionData.Name
            };
        }
    }
}
