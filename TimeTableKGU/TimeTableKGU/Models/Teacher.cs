using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Full_Name { get; set; }
        public Teacher() { }
        public Teacher( string l, string p, string ps, string d, string f, int i=0)
        {
            TeacherId = i;
            Login = l;
            Password = p;
            Position = ps;
            Department = d;
            Full_Name = f;
        }
    }
}
