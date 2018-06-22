using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using UKLON.TestTask.Structs;

namespace UKLON.TestTask.IntegrationAdapter
{
    internal static class YandexMappingResult
    {

        public static readonly Dictionary<int, Result> MappingYandexResult = new Dictionary<int, Result>()
        {
            { 200, Result.Success },
            { 404, Result.NotFoundError }, //// Если нужна специфическая обработка для конкретніх кодов, их можно добавить в маппинг.
        };
    }

    [XmlRoot("info")]
    public class FullRegionInfo
    {
        [XmlElement("region")]
        public RegionInfo Region { get; set; }

        [XmlAttribute("lang")]
        public string Language { get; set; }

        [XmlElement("traffic")]
        public TrafficInfo Traffic { get; set; }

        public static explicit operator RegionTrafficInfo(FullRegionInfo regionInfo)
        {
            if ( regionInfo?.Region == null )
                return null;

            return new RegionTrafficInfo()
            {
                Id = (int)regionInfo.Region.Id,
                Name = regionInfo.Region.Name,
                Level = regionInfo.Traffic.RegionInfo?.Level,
                Icon = regionInfo.Traffic.RegionInfo?.Icon,
                Description = regionInfo.Traffic.RegionInfo?.Description?.FirstOrDefault(x => x.Language == regionInfo.Language).Value
            };
        }

        public static explicit operator RegionTrafficInfoWithStatus(FullRegionInfo regionInfo)
        {
            if (regionInfo?.Region == null)
                return null;

            return new RegionTrafficInfoWithStatus()
            {
                Id = (int)regionInfo.Region.Id,
                Name = regionInfo.Region.Name,
                Level = regionInfo.Traffic.RegionInfo?.Level,
                Icon = regionInfo.Traffic.RegionInfo?.Icon,
                Description = regionInfo.Traffic.RegionInfo?.Description?.FirstOrDefault(x => x.Language == regionInfo.Language).Value
            };
        }
    }
    public class RegionInfo
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("title")]
        public string Name { get; set; }

    }

    public class TrafficInfo
    {
        [XmlElement("region")]
        public RegionTraffic RegionInfo { get; set; }

    }

    public class RegionTraffic
    {
        [XmlElement("level")]
        public int Level { get; set; }

        [XmlElement("icon")]
        public string Icon { get; set; }

        [XmlElement("hint")]
        public TrafficDescription[] Description { get; set; }

    }
    public class TrafficDescription
    {

        [XmlAttribute("lang")]
        public string Language { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
