using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Models
{
    public class Student
    {
        public int? StudentId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Group { get; set; }
        public int? Subgroup { get; set; }
        public string Full_Name { get; set; }
        public bool Group_Leader { get; set; }
        public Student() {  }
        public Student( string l, string p, int g, int s, string f, bool gl, int k=0)

        {
            StudentId = k;
            Login = l;
            Password = p;
            Group = g;
            Subgroup = s;
            Full_Name = f;
            Group_Leader = gl;
        }
    }
}
