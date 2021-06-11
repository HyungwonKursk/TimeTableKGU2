using System;
using System.Collections.Generic;
using System.Text;
using TimeTableKGU.DataBase;
using Xamarin.Forms;


namespace TimeTableKGU.Views
{
    public partial class AuthorizationPage : ContentPage
    {
        public class ClientControls
        {
            public Label NameLab { get; set; }
            public Label LoginLab { get; set; }
            public Label GroupLab { get; set; } // группа или кафедра
            public Button SettingBtn { get; set; } //кнопка показать список группы или добавить ссылку
            public Button LoginOutBtn { get; set; }
            public static string CurrentUser { get; set; }

            public ClientControls()
            {
                NameLab = new Label
                {
                    Style = Device.Styles.TitleStyle,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                GroupLab = new Label
                {
                    Style = Device.Styles.TitleStyle,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                SettingBtn = new Button
                {

                    TextColor = Color.Black,
                    BorderColor = Color.Black,
                };
                LoginOutBtn = new Button
                {
                    Text = "Выйти из учётной записи",

                    TextColor = Color.Black,
                    BorderColor = Color.Black,
                };


            }


        }
        private async void SettingBtn_Clicked(object sender, EventArgs e)
        {

            if (ClientControls.CurrentUser == "Студент")
                GetGroupPage();
            else
                await Navigation.PushAsync(new LinkPage());

        }
        public ClientControls ClientPage;

        public void GetClientPage()
        {
            ClientPage = new ClientControls();
            ClientPage.SettingBtn.Clicked += SettingBtn_Clicked;

            if (ClientControls.CurrentUser == "Студент")
            {
                var student = DbService.LoadAllStudent();
                if (student == null) return;
                Title = student[0].Login;
                ClientPage.NameLab.Text = "ФИО: " + student[0].Full_Name;
                ClientPage.GroupLab.Text = "Группа: " + Convert.ToString(student[0].Group) + "." + Convert.ToString(student[0].Subgroup);
                ClientPage.SettingBtn.Text = "Список группы";
            }
            else
                if (ClientControls.CurrentUser == "Преподаватель")
            {
                var teacher = DbService.LoadAllTeacher();
                if (teacher == null) return;
                Title = teacher[0].Login;
                ClientPage.NameLab.Text = "ФИО: " + teacher[0].Full_Name;
                ClientPage.GroupLab.Text = "Кафедра: " + teacher[0].Department;
                ClientPage.SettingBtn.Text = "Прикрепить личную ссылку для занятий";
            }
            LoginPage = null;
            RegisrationPage = null;

            ClientPage.LoginOutBtn.Clicked += GoLoginPage;

            StackLayout stackLayout = new StackLayout();
            stackLayout.Margin = 20;

            stackLayout.Children.Add(ClientPage.NameLab);
            stackLayout.Children.Add(ClientPage.GroupLab);
            stackLayout.Children.Add(ClientPage.SettingBtn);

            stackLayout.Children.Add(ClientPage.LoginOutBtn);

            this.Content = new ScrollView { Content = stackLayout };

            #region ToolBarItems

            this.ToolbarItems.Clear();


            ToolbarItem LogOut = new ToolbarItem
            {
                Text = "Выйти",
                Order = ToolbarItemOrder.Secondary,
                Priority = 1,
            };

            LogOut.Clicked += GoLoginPage;
            this.ToolbarItems.Add(LogOut);

            #endregion
        }
        public void GoLoginPage(object sender, EventArgs args)
        {
            this.ToolbarItems.Clear();

            if (ClientControls.CurrentUser == "Студент")
                if (!DbService.isEmptyStudent())
                {
                    var st = DbService.LoadAllStudent();
                    DbService.RemoveStudent(st[0]);
                }

            if (ClientControls.CurrentUser == "Преподаватель")
                if (!DbService.isEmptyTeacher())
                {
                    var th = DbService.LoadAllTeacher();
                    DbService.RemoveTeacher(th[0]);
                }

            ClientControls.CurrentUser = "";
            var tt = DbService.LoadAllTimeTable();
            DbService.RemoveTimeTable(tt);
            TimeTablePage.Type = "";
            ClientPage = null;
            GetLoginPage();
        }
    }
}
