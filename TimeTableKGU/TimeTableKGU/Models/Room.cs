using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public int Room_Number { get; set; }
        public int Capacity { get; set; }
        public string Room_Type { get; set; }
        public bool Projector { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public Room()
        {
            Lessons = new List<Lesson>();
        }
    }
}
