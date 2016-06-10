using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Shold_Community
{

    public class PatternService
    {
        public int id { get; set; }
        public int playerId { get; set; }
        public string name { get; set; }
        public string typeCastle { get; set; }
        public int accessFrom { get; set; }
        public String getFromServer()
        {

            return Initial.GetPatterns();
        }
    }
}
