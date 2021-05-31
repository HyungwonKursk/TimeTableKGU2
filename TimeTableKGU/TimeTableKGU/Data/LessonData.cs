using System;
using System.Collections.Generic;
using System.Text;
using TimeTableKGU.Models;

namespace TimeTableKGU.Data
{
    public class LessonData
    {
        public static List<Lesson> Lessons;
        public LessonData()
        {
            Lessons = new List<Lesson>();
        }
    }
}
