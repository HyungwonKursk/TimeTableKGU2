using System;
using System.Collections.Generic;
using TimeTableKGU.Models;

namespace TimeTableKGU.Data
{
    public class StudentData
    {
        public static List<Student> Students;
       // public static Student CurrentUser{get;set;}
        public StudentData()
        {
            Students = new List<Student>();
            //CurrentUser = new Student(1, "student1", "123456", 413, 1, "Иванов Иван", false);
        }

    }
}
