using System.Collections.Generic;
using TimeTableKGU.Models;
namespace TimeTableKGU.Data
{
    public class TimeTableData
    {
        public static List<TimeTable> TimeTables;
        static TimeTableData()
        {
            TimeTables = new List<TimeTable>();
            
        }
    }
}
