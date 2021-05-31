using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public int Group_number { get; set; }
        public int Course { get; set; }
        public int Subgroups_number { get; set; }
        public Direction Direction { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Student> Students { get; set; }
        public Group()
        {
            Lessons = new List<Lesson>();
            Students = new List<Student>();
        }

    }
}
