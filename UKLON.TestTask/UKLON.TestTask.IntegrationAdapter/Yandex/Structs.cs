using System;
using System.Globalization;
using System.Runtime.Serialization;
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
            { 404, Result.NotFoundError },
        };
    }

    public class BaseYandexResponse
    {

    }

    [XmlRoot("info")]
    public class FullRegionInfo : BaseYandexResponse
    {
        [XmlAttribute("lang")]
        public string Language { get; set; }

        [XmlElement("traffic")]
        public TrafficInfo Traffic { get; set; }

        public static explicit operator RegionTrafficInfo(FullRegionInfo regionInfo)
        {
            if (regionInfo == null)
                return null;

            return new RegionTrafficInfo()
            {
                Id = (int)regionInfo?.Traffic?.Region?.Id,
                Name = regionInfo?.Traffic?.Name,
                Level = regionInfo?.Traffic?.Region?.Level,
                Icon = regionInfo?.Traffic?.Region?.Icon,
                Description = ""
            };
        }
    }

    public class TrafficInfo
    {
        [XmlElement("title")]
        public string Name { get; set; }

        [XmlElement("region")]
        public RegionInfo Region { get; set; }

    }

    public class RegionInfo
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("level")]
        public int Level { get; set; }

        [XmlElement("icon")]
        public string Icon { get; set; }

        [XmlElement("hint")]
        public List<TrafficDescription> Description { get; set; }
    }
    public class TrafficDescription
    {
        [XmlAttribute("lang")]
        public string Text { get; set; }
    }
}
