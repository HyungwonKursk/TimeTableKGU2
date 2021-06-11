using System;
using Microsoft.EntityFrameworkCore;
using TimeTableKGU.Models;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TimeTableKGU.DataBase
{
    public class ApplicationContext : DbContext
    {

        // списки подключенных таблиц
       
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }

        private string databaseName;

        public ApplicationContext(string databasePath = "database.db")
        {
            databaseName = databasePath;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TimeTable>().HasKey(m => m.TimeTableId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String databasePath =
              Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);

            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}
