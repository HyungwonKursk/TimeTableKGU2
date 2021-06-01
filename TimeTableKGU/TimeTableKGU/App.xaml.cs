using System;
using TimeTableKGU.DataBase;
using TimeTableKGU.Data;
using TimeTableKGU.Models;

using TimeTableKGU.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableKGU
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            // создать базу данных если ее еще нет
            DbService.RefrashDb(false);

            // загрузить все данные из базы
           DbService.LoadAll();

            MainPage = new AppShell();
           
            

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
