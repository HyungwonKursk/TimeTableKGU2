using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public Subject()
        {
            Lessons = new List<Lesson>();
        }
    }
}
