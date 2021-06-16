using System;
using System.Collections.Generic;
using TimeTableKGU.Models;
using TimeTableKGU.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static TimeTableKGU.Views.AuthorizationPage;

namespace TimeTableKGU.DataBase
{
    public static class DbService
    {
        private static ApplicationContext db = new ApplicationContext();

        /// <summary>
        /// Обновление базы данных
        /// </summary>
        /// <param name="delete">если true очитстить текущую базу</param>
        public static void RefrashDb(bool delete = false)
        {
            // Удаляем бд, если она существуеты
            if (delete)
                db.Database.EnsureDeleted();

            // Создаем бд, если она отсутствует
            db.Database.EnsureCreated();

        }

        public static void SaveDb()
        {
            db.SaveChanges();
        }


        /// <summary>
        /// Загрузить все данные из базы в память (при запуске)
        /// </summary>
        public static void LoadAll()
        {
            if (!isEmptyStudent())
            {
                StudentData.Students = LoadAllStudent();
               ClientControls.CurrentUser = "Студент";
                TimeTableData.TimeTables = LoadAllTimeTable();
            }
            if (!isEmptyTeacher())
            {
                TeacherData.Teachers = LoadAllTeacher();
                ClientControls.CurrentUser = "Преподаватель";
                TimeTableData.TimeTables = LoadAllTimeTable();
            }

        }
        #region TimeTable
        public static void AddTimeTable(TimeTable timetable)
        {
            if (timetable == null) return;
            db.TimeTables.Add(timetable);
            db.SaveChanges();
        }
        public static void AddTimeTable(List<TimeTable> timetables)
        {

            if (timetables == null) return;

            foreach (var timetable in timetables)
                db.TimeTables.Add(timetable);

            db.SaveChanges();

        }
        public static void RemoveTimeTable(TimeTable timetable)
        {
            if (timetable == null) return;
            db.TimeTables.Remove(timetable);

            db.SaveChanges();
        }

        public static void RemoveTimeTable(List<TimeTable> timetables)
        {
            foreach (var timetable in timetables)
            {
                db.TimeTables.Remove(timetable);
            }
            db.SaveChanges();
        }
        public static List<TimeTable> LoadAllTimeTable()
        {
            return db.TimeTables.ToList();
        }
        #endregion

        #region Student
        public static bool isEmptyStudent()
        {
            if (db.Students.Count() == 0)
                return true;
            else return false;
        }
        public static void AddStudent(Student student)
        {
            if (student == null) return;
            db.Students.Add(student);
            db.SaveChanges();
        }

        public static void RemoveStudent(Student student)
        {
            if (student == null) return;
            db.Students.Remove(student);
            db.SaveChanges();
        }

        public static List<Student> LoadAllStudent()
        {
            return db.Students.ToList();
        }

        #endregion

        #region Teacher
        public static bool isEmptyTeacher()
        {
            if (db.Teachers.Count() == 0)
                return true;
            else return false;
        }
        public static void AddTeacher(Teacher teacher)
        {
            if (teacher == null) return;
            db.Teachers.Add(teacher);
            db.SaveChanges();
        }
        public static void RemoveTeacher(Teacher teacher)
        {
            if (teacher == null) return;
            db.Teachers.Remove(teacher);
            db.SaveChanges();
        }

        public static List<Teacher> LoadAllTeacher()
        {
            return db.Teachers.ToList();
        }

        #endregion
    }
}

