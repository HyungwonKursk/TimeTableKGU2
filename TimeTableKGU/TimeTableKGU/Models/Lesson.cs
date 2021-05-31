using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public int? ClassroomId { get; set; }
        public int? Lesson_number { get; set; }
        public int? DisciplineId { get; set; }
        public string Lesson_type { get; set; }
        public int? WeekId { get; set; }
        public int? GroupId { get; set; }
        public int? TeacherId { get; set; }
        public DateTime Lesson_start { get; set; }
        public DateTime Lesson_finish { get; set; }
        public string Link { get; set; }
        public Room Classroom { get; set; }
        public Subject Subject { get; set; }
        public Week Week { get; set; }
        public Group Group { get; set; }
        public Teacher Teacher { get; set; }
    }
}
