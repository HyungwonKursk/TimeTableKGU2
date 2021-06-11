using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Models
{
    public class Link
    {
        public int group { get; set; }
        public string link { get; set; }
        
        public Link(string _link, int _group)
        {
            link = _link;
            group = _group;

        }
    }
}
