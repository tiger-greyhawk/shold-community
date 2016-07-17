using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Shold_Community.Entities
{
    [DataContract]
    class PatternFormation
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int patternId { get; set; }
        [DataMember]
        public string fileName { get; set; }
        [DataMember]
        public string file { get; set; }
    }
}
