﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shold_Community
{


    public class Patterns
    {
        public Pattern[] Property { get; set; }
    }

    public class Pattern
    {
        public int id { get; set; }
        public int playerId { get; set; }
        public string name { get; set; }
        public string typeCastle { get; set; }
        public int accessFrom { get; set; }
    }


    /*
        public class Patterns
        {
            public int id { get; set; }
            public int playerId { get; set; }
            public string name { get; set; }
            public string typeCastle { get; set; }
            public int accessFrom { get; set; }
        }

        */
}
