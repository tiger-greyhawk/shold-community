using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shold_Community.Entities.Resources
{
    class SendResources
    {
        public SendRes[] Property1 { get; set; }
    }

    public class SendRes
    {
        public int id { get; set; }
        public int resourceId { get; set; }
        public int amount { get; set; }
        public int playerId { get; set; }
        public long timestamp { get; set; }
        public long currentTimestamp { get; set; }


    }
}
