using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKLON.TestTask.Structs
{
    public class RegionTrafficInfo
    { 
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? Level { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }
    }


    public class RegionTrafficInfoWithStatus : RegionTrafficInfo
    {
        public string Code { get; set; }

        public string Text { get; set; }
    }
}
