using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shold_Community.Entities
{
    class Villages
    {
        public Village[] Property1 { get; set; }
    }

    public class Village
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        public int idInWorld { get; set; }
    }
}
