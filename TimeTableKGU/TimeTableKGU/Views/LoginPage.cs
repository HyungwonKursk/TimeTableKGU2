using System;
using System.Collections.Generic;
using System.Text;
using TimeTableKGU.Data;
using TimeTableKGU.DataBase;
using TimeTableKGU.Interface;
using TimeTableKGU.Web;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableKGU.Views
{
    public partial class AuthorizationPage : ContentPage
    {
        public class LoginControls
        {
            public Image LogoImage { get; set; }
            public Label TitleLab { get; set; }
            public Entry LoginBox { get; set; }
            public Entry PasswBox { get; set; }
            public Button LoginBtn { get; set; }
            public Button RegisBtn { get; set; }

            public LoginControls()
            {
                LogoImage = new Image
                {
                    Source = "Logotip.jpg"
                };
                TitleLab = new Label
                {
                    Text = "Вход",
                   
                    Style = Device.Styles.TitleStyle,
                    HorizontalOptions = LayoutOptions.Center,
                };

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
                    IsPassword = true,
                    ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                    Style = Device.Styles.BodyStyle,
                    HorizontalOptions = LayoutOptions.Fill
                };

                LoginBtn = new Button
                {
                    Text = "Войти",
                    //BackgroundColor = Color.FromHex("#BFD9B6"),
                    TextColor = Color.White,
                    BorderColor = Color.Black,
                };
                RegisBtn = new Button
                {
                    Text = "Зарегистрироваться",
                    //BackgroundColor = Color.FromHex("#BFD9B6"),
                    TextColor = Color.White,
                    BorderColor = Color.Black,
                };
            }

            public StackLayout SetContent()
            {
                StackLayout stackLayout = new StackLayout();
                stackLayout.Margin = 20;

                stackLayout.Children.Add(new BoxView { VerticalOptions = LayoutOptions.CenterAndExpand });
                stackLayout.Children.Add(LogoImage);
                //stackLayout.Children.Add(TitleLab);
                stackLayout.Children.Add(LoginBox);
                stackLayout.Children.Add(PasswBox);
                stackLayout.Children.Add(LoginBtn);
                stackLayout.Children.Add(RegisBtn);
                stackLayout.Children.Add(new BoxView { VerticalOptions = LayoutOptions.CenterAndExpand });

                return stackLayout;
            }
        }

        private LoginControls LoginPage;
        public void GetLoginPage()
        {
            Title = "Войти";

            LoginPage = new LoginControls();

            LoginPage.LoginBtn.Clicked += LoginIn;
            LoginPage.RegisBtn.Clicked += ToRegistrationPage;
            this.Content = new ScrollView {Content= LoginPage.SetContent() };
            //this.Content = LoginPage.SetContent();
        }

        private bool isLoading = false;
        public async void LoginIn(object sender, EventArgs e)
        {
            if (LoginPage.LoginBox.Text == "" || LoginPage.PasswBox.Text == "")
            {
                DependencyService.Get<IToast>().Show("Введены не все поля");
                return;
            }
            if (isLoading)
            {
                DependencyService.Get<IToast>().Show("Пользователь уже загружается");
                return;
            }

            bool connect = await WebData.CheckConnection();
            if (connect == false) return;

            isLoading = true;

            var userStudent = await new UserService().AuthrizationStudent(LoginPage.LoginBox.Text, LoginPage.PasswBox.Text);
            var userTeacher = await new UserService().AuthrizationTeacher(LoginPage.LoginBox.Text, LoginPage.PasswBox.Text);
            if (userStudent != null) // если сервер вернул данные пользователя - загрузить в пользователя
            {
                DbService.AddStudent(userStudent); // сохранили пользователя
                StudentData.Students=DbService.LoadAllStudent();
                ClientControls.CurrentUser = "Студент";
                isLoading = false;
                var timetable = await new TimeTableService().GetStudentTimeTable(userStudent.Group, userStudent.Subgroup, userStudent.StudentId);
                TimeTableData.TimeTables = timetable;
                DbService.AddTimeTable(timetable);
                GetClientPage();
                return;
            }
            else
            if (userTeacher != null)
            {
                DbService.AddTeacher(userTeacher); // сохранили пользователя
                ClientControls.CurrentUser = "Преподаватель";
                isLoading = false;
                var timetable = await new TimeTableService().GetTeacherTimeTable(userTeacher.TeacherId);
                TimeTableData.TimeTables = timetable;
                DbService.AddTimeTable(timetable);
                GetClientPage();
                isLoading = false;
                return;
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");
                isLoading = false;
                return;
            }
        }
        private void ToRegistrationPage(object sender, EventArgs e)
        {
            if (isLoading)
            {
                DependencyService.Get<IToast>().Show("Дождитесь окончания загрузки");
                return;
            }
            GetRegistrationPage();
        }

    }
}
