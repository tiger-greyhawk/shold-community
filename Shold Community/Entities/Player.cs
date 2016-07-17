using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shold_Community.Entities
{

    public class Player
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string nick { get; set; }
        public int invite { get; set; }
        public string motivater { get; set; }
        //public DateTime lastRequest { get; set; }
    }

}
