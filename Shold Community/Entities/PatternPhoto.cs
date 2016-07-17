using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Shold_Community.Entities
{
    [DataContract]
    class PatternPhoto
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int patternId { get; set; }
        [DataMember]
        public string photoName { get; set; }
        [DataMember]
        public string photo { get; set; }

    }
}
