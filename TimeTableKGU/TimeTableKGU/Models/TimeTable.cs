using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Models
{
    public class TimeTable
    {
        public int TimeTableId { get; set; }
        public string Week_day { get; set; }
        public string Time { get; set; }
        public string Subject { get; set; }
        public string Name_Group { get; set; }
        public int Room_Number { get; set; }
        public string Link { get; set; }
        public string Parity { get; set; }//числитель или знаменатель
        public TimeTable( string _day,string _time,string _subject,string _name_group,int _room, string _link,string _parity, int k = 0) : this()
        {
            TimeTableId =k;
            Week_day = _day;
            Time = _time;
            Subject = _subject;
            Name_Group = _name_group;
            Room_Number = _room;
            Link = _link;
            Parity = _parity;
        }
        public TimeTable()
        {
            TimeTableId = 0;
            Week_day = "";
            Time = "";
            Subject = "";
            Name_Group = "";
            Room_Number = 0;
            Link = "";
            Parity = "";
        }
    }
}
