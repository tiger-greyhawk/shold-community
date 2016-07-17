using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Shold_Community.Entities
{
    //[DataContract]
    class ArmysToAttack
    {

        public class Rootobject
        {
            public ArmyToAttack[] Property1 { get; set; }
        }
        [DataContract]
        public class ArmyToAttack
        {
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string secret { get; set; }
            [DataMember]
            public string type { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public int villageId { get; set; }
            [DataMember]
            public int timeTo { get; set; }
            [DataMember]
            public int card { get; set; }
            [DataMember]
            public int playerId { get; set; }
            [DataMember]
            public long timestamp { get; set; }
            [DataMember]
            public long currentTimestamp { get; set; }
        }

    }
}
