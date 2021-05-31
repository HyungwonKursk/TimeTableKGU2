using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTableKGU.DataBase;
using TimeTableKGU.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;
using TimeTableKGU.Data;

namespace TimeTableKGU.DataBase.Tests
{
    [TestClass()]
    public class DbServiceTests
    {

        [TestMethod()]
        public void AddNotEmptyTimeTableTest()
        {
            TimeTable t = new TimeTable("Понедельник", "13.20-14.50", "Основы теории нейронных сетей(лек.)", "Добрица В.П.", 0, "ССЫЛКА", "Числитель");
            DbService.AddTimeTable(t);

        }

        [TestMethod()]
        public void AddEmptyTimeTableTest()
        {
            TimeTable t = new TimeTable();
            DbService.AddTimeTable(t);

        }

        [TestMethod()]
        public void AddListTimeTableTest()
        {
            DbService.RefrashDb(true);
            List<TimeTable> tables = new List<TimeTable>();
            tables = TimeTableData.TimeTables;
            DbService.AddTimeTable(tables);

        }
        [TestMethod()]
        public void AddNullTimeTableTest()
        {
            TimeTable t = null;
            DbService.AddTimeTable(t);

        }
        [TestMethod()]
        public void RemoveTimeTableTest()
        {
            DbService.RefrashDb(true);

            TimeTable t = new TimeTable("Понедельник", "13.20-14.50", "Основы теории нейронных сетей(лек.)", "Добрица В.П.", 0, "ССЫЛКА", "Числитель");
            DbService.AddTimeTable(t);
            DbService.RemoveTimeTable(t);

        }
        [TestMethod()]
        public void RemoveNullTimeTableTest()
        {
            TimeTable t = null;
            DbService.RemoveTimeTable(t);

        }

        [TestMethod()]
        public void RemoveTimeTablesTest()
        {
            DbService.RefrashDb(true);
            List<TimeTable> tables = new List<TimeTable>();
            tables = TimeTableData.TimeTables;
            DbService.AddTimeTable(tables);
            DbService.RemoveTimeTable(tables);

        }

        [TestMethod()]
        public void MyClassInitializeTrue()
        {
            //MyClassInitialize
            DbService.RefrashDb(true);
        }

        [TestMethod()]
        public void MyClassInitializeFalse()
        {
            //MyClassInitialize
            DbService.RefrashDb(false);
        }

        [TestMethod()]
        public void MyClassInitializeZero()
        {
            //MyClassInitialize
            DbService.RefrashDb();
        }

        [TestMethod()]
        public void AddNotEmptyStudentTest()
        {
            Student s = new Student("log", "pass", 413, 1, "FIO", true);
            DbService.AddStudent(s);

        }

        [TestMethod()]
        public void AddEmptyStudentTest()
        {
            Student s = new Student();
            DbService.AddStudent(s);

        }


        [TestMethod()]
        public void AddNullStudentTest()
        {
            Student s = null;
            DbService.AddStudent(s);

        }
        [TestMethod()]
        public void RemoveStudentTest()
        {
            DbService.RefrashDb(true);

            Student s = new Student("log", "pass", 413, 1, "FIO", true);
            DbService.AddStudent(s);
            DbService.RemoveStudent(s);

        }
        [TestMethod()]
        public void RemoveNullStudentTest()
        {
            Student s = null;
            DbService.RemoveStudent(s);

        }

        [TestMethod()]
        public void AddNotEmptyTeacherTest()
        {
            Teacher teacher = new Teacher("log", "pass", "pos", "depart", "FIO");
            DbService.AddTeacher(teacher);

        }

        [TestMethod()]
        public void AddEmptyTeacherTest()
        {
            Teacher teacher = new Teacher();
            DbService.AddTeacher(teacher);

        }


        [TestMethod()]
        public void AddNullTeacherTest()
        {
            Teacher teacher = null;
            DbService.AddTeacher(teacher);

        }
        [TestMethod()]
        public void RemoveTeacherTest()
        {
            DbService.RefrashDb(true);

            Teacher teacher = new Teacher("log", "pass", "pos", "depart", "FIO");
            DbService.AddTeacher(teacher);

            DbService.RemoveTeacher(teacher);

        }
        [TestMethod()]
        public void RemoveNullTeacherTest()
        {
            Teacher teacher = null;
            DbService.RemoveTeacher(teacher);

        }
        [TestMethod()]
        public void LoadAllTimeTableTest()
        {
            List<TimeTable> l = DbService.LoadAllTimeTable();

        }

        [TestMethod()]
        public void LoadAllStudentTest()
        {
            List<Student> l = DbService.LoadAllStudent();

        }
        [TestMethod()]
        public void LoadAllTeacherTest()
        {
            List<Teacher> l = DbService.LoadAllTeacher();

        }

        
    }
}