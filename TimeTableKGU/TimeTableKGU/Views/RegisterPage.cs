using TimeTableKGU.Models;

using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using TimeTableKGU.Interface;
using TimeTableKGU.Web;
using TimeTableKGU.Web.Services;
using TimeTableKGU.DataBase;
using TimeTableKGU.Data;

namespace TimeTableKGU.Views
{
    public partial class AuthorizationPage : ContentPage
    {
        public class RegisrationContrioolers
        {
            public Label labelMessage { get; set; }
            public Entry NameBox { get; set; }
            public Entry LoginBox { get; set; }
            public Entry PasswBox { get; set; }
            public Entry PasswCheckBox { get; set; }
            public Button RegisBtn { get; set; }
            public Button LoginBtn { get; set; }
            public Picker TypePick { get; set; }
            public Picker SubPick { get; set; }
            public Picker GroupPick { get; set; }
            public Entry DepartBox { get; set; }
            public Entry PosBox { get; set; }
            public Grid grid { get; set; }
            public Label label { get; set; }
            public CheckBox checkBox { get; set; }
            public RegisrationContrioolers()
            {
                checkBox = new CheckBox {};
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                grid = new Grid {
                    ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 45},
                    new ColumnDefinition { Width = 250 },
                }
                };
                label = new Label { Text = "Согласие на обработку персональных данных"};

                grid.Children.Add(checkBox,0,0);
                grid.Children.Add(label, 1, 0);

                labelMessage = new Label
                {
                    Text = "Регистрация",
                    Style = Device.Styles.TitleStyle,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Color.Black
                };

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
                TypePick = new Picker { Title = "Должность", TextColor = Color.Black };
                TypePick.Items.Add("Студент");
                TypePick.Items.Add("Преподаватель");

                GroupPick = new Picker { Title = "Группа", TextColor = Color.Black };

                SubPick = new Picker { Title = "Подгруппа", TextColor = Color.Black };
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

                RegisBtn = new Button
                {
                    Text = "Зарегистрироваться",
                    IsEnabled = false,
                    BackgroundColor = Color.FromHex("#b3e5fc"),
                    TextColor = Color.Black,
                    BorderColor = Color.Black,
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
                PosBox = new Entry
                {
                    Text = "",
                    Placeholder = "Должность",
                    Keyboard = Keyboard.Default,
                    TextColor = Color.Black,
                    PlaceholderColor = Color.Black,
                    ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                    Style = Device.Styles.BodyStyle,
                    HorizontalOptions = LayoutOptions.Fill
                };
                LoginBtn = new Button
                {
                    Text = "Уже есть учетная запись",
                    BackgroundColor = Color.FromHex("#b3e5fc"),
                    TextColor = Color.Black,
                    BorderColor = Color.Black,
                };
                TypePick.SelectedIndexChanged += TypePick_Change;
            }

            private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
            {
                if (checkBox.IsChecked)
                    RegisBtn.IsEnabled = true;
                if (!checkBox.IsChecked)
                    RegisBtn.IsEnabled = false;

            }

            private void TypePick_Change(object sender, EventArgs e)
            {
                if (TypePick.Items[TypePick.SelectedIndex] == "Студент")
                {
                    stackLayout.Children.Remove(grid);
                    stackLayout.Children.Remove(PosBox);
                    stackLayout.Children.Remove(DepartBox);
                    stackLayout.Children.Remove(LoginBtn);
                    stackLayout.Children.Remove(RegisBtn);
                    stackLayout.Children.Add(GroupPick);
                    stackLayout.Children.Add(SubPick);
                    stackLayout.Children.Add(grid);
                }

                else
                if (TypePick.Items[TypePick.SelectedIndex] == "Преподаватель")
                {
                    stackLayout.Children.Remove(grid);
                    stackLayout.Children.Remove(GroupPick);
                    stackLayout.Children.Remove(SubPick);
                    stackLayout.Children.Remove(LoginBtn);
                    stackLayout.Children.Remove(RegisBtn);
                    stackLayout.Children.Add(DepartBox);
                    stackLayout.Children.Add(PosBox);
                    stackLayout.Children.Add(grid);
                }

                stackLayout.Children.Add(RegisBtn);
                stackLayout.Children.Add(LoginBtn);
                //stackLayout.Children.Add(new BoxView { VerticalOptions = LayoutOptions.CenterAndExpand });
            }

            StackLayout stackLayout = new StackLayout();
            public ScrollView SetContent()
            {
                
                stackLayout.Margin = 20;

                stackLayout.Children.Add(new BoxView { VerticalOptions = LayoutOptions.CenterAndExpand });
                stackLayout.Children.Add(labelMessage);
                stackLayout.Children.Add(NameBox);
                stackLayout.Children.Add(TypePick);
                stackLayout.Children.Add(LoginBox);
                stackLayout.Children.Add(PasswBox);
                stackLayout.Children.Add(PasswCheckBox);
                
               

                ScrollView scroll = new ScrollView { Content = stackLayout };
                return scroll;
            }
        }

        public RegisrationContrioolers RegisrationPage;

        public async void GetRegistrationPage()
        {
            Title = "Зарегистрироваться";

            RegisrationPage = new RegisrationContrioolers();
            var group = await new GroupService().GetNumbersOfGroups();

            for (int i = 0; i < group.Count; i++)
                RegisrationPage.GroupPick.Items.Add(group[i].ToString());

            RegisrationPage.SetContent();
            RegisrationPage.RegisBtn.Clicked += RegistrUser;
            RegisrationPage.LoginBtn.Clicked += ToLoginPage;

            this.Content = new ScrollView { Content = RegisrationPage.SetContent() };
        }
        public async void RegistrUser(object sender, EventArgs e)
        {
            if (isLoading)
            {
                DependencyService.Get<IToast>().Show("Пользователь уже загружается");
                return;
            }

            if (RegisrationPage.TypePick.Items[RegisrationPage.TypePick.SelectedIndex] == "Студент")
            {
                #region проверка введённых данных
                if (RegisrationPage.NameBox.Text == "" || RegisrationPage.LoginBox.Text == "" ||
                RegisrationPage.PasswBox.Text == "" || RegisrationPage.PasswCheckBox.Text == "" || RegisrationPage.GroupPick.SelectedIndex == -1 )
                {
                    DependencyService.Get<IToast>().Show("Не все поля заполнены"); return;
                }

                if (RegisrationPage.PasswBox.Text != RegisrationPage.PasswCheckBox.Text)
                {
                    DependencyService.Get<IToast>().Show("Пароли не совпадают"); return;
                }

                if (RegisrationPage.SubPick.SelectedIndex == -1)
                    RegisrationPage.SubPick.Items[RegisrationPage.SubPick.SelectedIndex] = "1";

                #endregion
                bool connect = await WebData.CheckConnection();
                if (connect == false) return;

                if (! await new CheckService().GetCheckLogin(RegisrationPage.LoginBox.Text))
                {
                    DependencyService.Get<IToast>().Show("Логин уже занят");
                    RegisrationPage.LoginBox.Text = ""; return;
                }
                isLoading = true;
                // отправка данных регистрации на сервер    
                var user = await new UserService().
                        RegisterStudent(RegisrationPage.LoginBox.Text, RegisrationPage.PasswBox.Text,
                        Convert.ToInt32(RegisrationPage.GroupPick.Items[RegisrationPage.GroupPick.SelectedIndex]),
                        Convert.ToInt32(RegisrationPage.SubPick.Items[RegisrationPage.SubPick.SelectedIndex]),
                        RegisrationPage.NameBox.Text);
                // если сервер вернул данные пользователя - загрузить в пользователя
                if (user != null)
                {
                    var timetable = await new TimeTableService().GetStudentTimeTable(user.Group,user.Subgroup,user.StudentId);
                    TimeTableData.TimeTables = timetable;
                    DbService.AddTimeTable(timetable);
                    DbService.AddStudent(user); // сохранили пользователя
                    StudentData.Students = DbService.LoadAllStudent();
                    ClientControls.CurrentUser = "Студент";
                    isLoading = false;
                    return;
                }
                else
                {
                    await DisplayAlert("Ошибка", "Сервер не вернул данные", "OK");
                    isLoading = false;
                    return;
                }
            }
            if (RegisrationPage.TypePick.Items[RegisrationPage.TypePick.SelectedIndex] == "Преподаватель")
            {
                #region проверка введённых данных
                if (RegisrationPage.NameBox.Text == "" || RegisrationPage.LoginBox.Text == "" ||
                RegisrationPage.PasswBox.Text == "" || RegisrationPage.PasswCheckBox.Text == "" || RegisrationPage.DepartBox.Text == "" || RegisrationPage.PosBox.Text == "")
                {
                    DependencyService.Get<IToast>().Show("Не все поля заполнены"); return;
                }

                if (RegisrationPage.PasswBox.Text != RegisrationPage.PasswCheckBox.Text)
                {
                    DependencyService.Get<IToast>().Show("Пароли не совпадают"); return;
                }


                #endregion
                bool connect = await WebData.CheckConnection();
                if (connect == false) return;

                //проверка занят ли логин
                if (!await new CheckService().GetCheckLogin(RegisrationPage.LoginBox.Text))
                {
                    DependencyService.Get<IToast>().Show("Логин уже занят");
                    RegisrationPage.LoginBox.Text = ""; return;
                }
                isLoading = true;
                // отправка данных регистрации на сервер    
                var user = await new UserService().
                        RegisterTeacher(RegisrationPage.LoginBox.Text, RegisrationPage.PasswBox.Text,
                        RegisrationPage.PosBox.Text,
                        RegisrationPage.DepartBox.Text, RegisrationPage.NameBox.Text);
                // если сервер вернул данные пользователя - загрузить в пользователя
                if (user != null)
                {
                    var teachers = await new TeacherService().GetTeachers();
                    int id = 0;
                    var st = user.Full_Name.Split(' ');
                    string name = st[0] + " " + st[1][0] + ". " + st[2][0] + ".";
                    for (int i = 0; i < teachers.Count; i++)
                    {
                        if (teachers[i].full_name == name)
                        {
                            id = teachers[i].id_t;
                            break;
                        }
                    }
                    var timetable = await new TimeTableService().GetTeacherTimeTable(id);
                    TimeTableData.TimeTables = timetable;
                    DbService.AddTimeTable(timetable);
                    DbService.AddTeacher(user); // сохранили пользователя
                    ClientControls.CurrentUser = "Преподаватель";

                    GetClientPage();
                    isLoading = false;
                    return;
                }
                else
                {
                    await DisplayAlert("Ошибка", "Сервер не вернул данные", "OK");
                    isLoading = false;
                    return;
                }
            }




        }
        private void ToLoginPage(object sender, EventArgs e)
        {
            if (isLoading)
            {
                DependencyService.Get<IToast>().Show("Дождитесь окончания загрузки");
                return;
            }
            GetLoginPage();
        }
    }
}