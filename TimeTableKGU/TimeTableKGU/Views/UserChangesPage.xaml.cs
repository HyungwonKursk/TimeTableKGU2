using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Data;
using TimeTableKGU.DataBase;
using TimeTableKGU.Interface;
using TimeTableKGU.Models;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TimeTableKGU.Views.AuthorizationPage;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserChangesPage : ContentPage
    {
        public Label labelMessage { get; set; }
        public Entry NameBox { get; set; }
        public Entry LoginBox { get; set; }
        public Entry PasswBox { get; set; }
        public Entry PasswCheckBox { get; set; }
        public Button ChangeBtn { get; set; }
        public Picker SubPick { get; set; }
        public Picker GroupPick { get; set; }
        public Entry DepartBox { get; set; }
      
        public UserChangesPage()
        {
            InitializeComponent();
            NameBox = new Entry
            {
                Text = "",
                Placeholder = "ФИО пользователя",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };

            GroupPick = new Picker { Title = "Группа", TextColor = Color.Black, Style = Device.Styles.BodyStyle, BackgroundColor = Color.FromHex("#4682B4") };

            SubPick = new Picker { Title = "Подгруппа", TextColor = Color.Black, Style = Device.Styles.BodyStyle, BackgroundColor = Color.FromHex("#4682B4") };
            SubPick.Items.Add("1");
            SubPick.Items.Add("2");


            LoginBox = new Entry
            {
                Text = "",
                Placeholder = "Логин",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            PasswBox = new Entry
            {
                Text = "",
                Placeholder = "Пароль",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            PasswCheckBox = new Entry
            {
                Text = "",
                Placeholder = "Подтверждение пароля",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                IsPassword = true,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            DepartBox = new Entry
            {
                Text = "",
                Placeholder = "Кафедра",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };

            ChangeBtn = new Button
            {
                Text = "Внести изменения",
                BackgroundColor = Color.FromHex("#b3e5fc"),
                TextColor = Color.Black,
                BorderColor = Color.Black,
            };
            ChangeBtn.Clicked += ChangeBtn_Clicked;
            SetPage();
        }

        private async void ChangeBtn_Clicked(object sender, EventArgs e)
        {
            if (PasswBox.Text != PasswCheckBox.Text)
            {
                DependencyService.Get<IToast>().Show("Введённые пароли не совпадают"); return;
            }
            if (ClientControls.CurrentUser == "Студент")
            {
                if ( NameBox.Text.IndexOf('.') != -1 || LoginBox.Text.IndexOf('.') != -1
                    || PasswBox.Text.IndexOf('.') != -1)
                {
                    DependencyService.Get<IToast>().Show("Поля не могут содержать символ '.'");return;
                }
                var dbstudent = DbService.LoadAllStudent();

                Student red_student = new Student(LoginBox.Text, PasswBox.Text,
                    0, 0, NameBox.Text, false);

                red_student.StudentId = dbstudent[0].StudentId;

                if (red_student.Full_Name == "")
                    red_student.Full_Name = dbstudent[0].Full_Name;
                if (red_student.Login=="")
                    red_student.Login= dbstudent[0].Login;
                if (red_student.Password == "")
                    red_student.Password = dbstudent[0].Password;
                if (GroupPick.SelectedIndex == -1)
                    red_student.Group = dbstudent[0].Group;
                else
                    red_student.Group = Convert.ToInt32(GroupPick.Items[GroupPick.SelectedIndex]);
                if (SubPick.SelectedIndex == -1)
                    red_student.Subgroup = dbstudent[0].Subgroup;
                else
                    red_student.Subgroup = Convert.ToInt32(SubPick.Items[SubPick.SelectedIndex]);

                var student = await new ChangeInfoService().ChangeStudent(red_student.StudentId, red_student.Login,
                red_student.Password, red_student.Group, red_student.Subgroup, red_student.Full_Name);
                
                DbService.RemoveStudent(dbstudent[0]);
                DbService.AddStudent(student);
                dbstudent = DbService.LoadAllStudent();
                StudentData.Students= dbstudent;
               

            }
            else
            {
                if (DepartBox.Text.IndexOf('.') != -1
                    || NameBox.Text.IndexOf('.') != -1 || LoginBox.Text.IndexOf('.') != -1
                    || PasswBox.Text.IndexOf('.') != -1)
                {
                    DependencyService.Get<IToast>().Show("Поля не могут содержать символ '.'");
                    return;
                }
                var dbteacher = DbService.LoadAllTeacher();

                Teacher red_teacher = new Teacher(LoginBox.Text, PasswBox.Text,"",
                    DepartBox.Text,NameBox.Text);
                red_teacher.TeacherId = dbteacher[0].TeacherId;

                if (red_teacher.Full_Name == "")
                    red_teacher.Full_Name = dbteacher[0].Full_Name;
                if (red_teacher.Login == "")
                    red_teacher.Login = dbteacher[0].Login;
                if (red_teacher.Password == "")
                    red_teacher.Password = dbteacher[0].Password;
                if (red_teacher.Position=="")
                    red_teacher.Position = dbteacher[0].Position;
                if (red_teacher.Department == "")
                    red_teacher.Department = dbteacher[0].Department;

                var teacher = await new ChangeInfoService().ChangeTeacher(dbteacher[0].TeacherId, red_teacher.Login,
                   red_teacher.Password, red_teacher.Department, red_teacher.Full_Name);
               
                DbService.RemoveTeacher(dbteacher[0]);
                DbService.AddTeacher(teacher);
                dbteacher = DbService.LoadAllTeacher();
                TeacherData.Teachers = dbteacher;

            }
            DependencyService.Get<IToast>().Show("Изменения выполнены");
        }

        public async void SetPage()
        {
            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(NameBox);
            stackLayout.Children.Add(LoginBox);
            stackLayout.Children.Add(PasswBox);
            stackLayout.Children.Add(PasswCheckBox);
            if (ClientControls.CurrentUser == "Студент")
            {
                var group = await new GroupService().GetNumbersOfGroups();

                for (int i = 0; i < group.Count; i++)
                    GroupPick.Items.Add(group[i].ToString());

                stackLayout.Children.Add(GroupPick);
                stackLayout.Children.Add(SubPick);

            }
            if (ClientControls.CurrentUser == "Преподаватель")
            {    
                stackLayout.Children.Add(DepartBox);
                
            }
            stackLayout.Children.Add(ChangeBtn);

            this.Content = new ScrollView { Content = stackLayout };
        }
    }
}