using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Models
{
    public class Week
    {
        public int WeekId { get; set; }
        public string Week_day { get; set; }
        public string Parity { get; set; }//числитель или знаменатель
        public ICollection<Lesson> Lessons { get; set; }
        public Week()
        {
            Lessons = new List<Lesson>();
        }

    }
}
