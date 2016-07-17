using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shold_Community.Entities
{
    class AdminMessages
    {

        public AdminMessage[] Property1 { get; set; }
    }

    public class AdminMessage
    {
        public int id { get; set; }
        public string russianMessage { get; set; }
        public string englishMessage { get; set; }
    }

}
