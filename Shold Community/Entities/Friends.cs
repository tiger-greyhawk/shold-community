using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shold_Community.Entities
{
    class Friends
    {
        public Friend[] Property1 { get; set; }
    }

    public class Friend
    {
        public int id { get; set; }
        public int playerId { get; set; }
        public int friendId { get; set; }
        public bool confirm { get; set; }
        public string comment { get; set; }
    }
}
