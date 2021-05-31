using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Models
{
    public class Direction
    {
        public int DirectionId { get; set; }
        public string Name { get; set; }
        public ICollection<Group> Groups { get; set; }
        public Direction()
        {
            Groups = new List<Group>();
        }
    }
}
