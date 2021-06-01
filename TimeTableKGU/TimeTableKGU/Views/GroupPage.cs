using System;
using System.Collections.Generic;
using System.Text;
using TimeTableKGU.Data;
using TimeTableKGU.DataBase;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;

namespace TimeTableKGU.Views
{
    public partial class AuthorizationPage : ContentPage
    {
        public class GroupControls
        {
            public ListView Students { get; set; }
            public Button Button { get; set; }
            public GroupControls()
            {
                Students = new ListView();
                Button = new Button
                {
                    Text = "Назад",
                    TextColor = Color.Black,
                    BorderColor = Color.Black,
                };
            }
        }

        public GroupControls GroupPage;

        public void GetGroupPage()
        {
            GroupPage = new GroupControls();

            Inisialize();
            GroupPage.Button.Clicked += GoToClientPage;
            ClientPage = null;
            StackLayout stackLayout = new StackLayout();
            stackLayout.Margin = 20;
            stackLayout.Children.Add(GroupPage.Students);

            stackLayout.Children.Add(GroupPage.Button);

            this.Content = new ScrollView { Content = stackLayout };
        }

        public async void Inisialize()
        {
            if (StudentData.Students == null)
                StudentData.Students = DbService.LoadAllStudent();
            var students = await new StudentService().GetStudents(StudentData.Students[0].Group);
            GroupPage.Students.ItemsSource = students;
        }

        public void GoToClientPage(object sender, EventArgs e)
        {
            GetClientPage();
        }
    }
}
