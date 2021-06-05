using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static TimeTableKGU.Views.AuthorizationPage;
using TimeTableKGU.Interface;
using TimeTableKGU.Data;
using TimeTableKGU.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TimeTableKGU.Web.Services;
using TimeTableKGU.DataBase;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeTablePage : ContentPage
    {
        Grid grid;
        public Button Mon { get; set; }
        public Button Tue { get; set; }
        public Button Wed { get; set; }
        public Button Thu { get; set; }
        public Button Fri { get; set; }
        public Button Sat { get; set; }
        public Button Update { get; set; }
        public Button Change { get; set; }
        public static string Type { get; set; }

        public TimeTablePage()
        {
            
            InitializeComponent();

            grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 60 },
                    new RowDefinition { Height = 60 },
                    new RowDefinition { Height = 60 },
                    new RowDefinition { Height = 60 },
                    new RowDefinition { Height = 60},
                    new RowDefinition { Height = 60 },
                    new RowDefinition { Height = 60 },
                    new RowDefinition { Height = 60 },
                    new RowDefinition { Height = 60 },
                    new RowDefinition { Height = 60 },
                    new RowDefinition { Height = 60 },
                    //new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    //new RowDefinition { Height = new GridLength(100, GridUnitType.Absolute) }
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 45},
                    new ColumnDefinition { Width = 250 },
                    new ColumnDefinition { Width = 50 }
                }

            };
            Mon = new Button { Text = "ПОНЕДЕЛЬНИК" };
            Tue = new Button { Text = "ВТОРНИК" };
            Wed = new Button { Text = "СРЕДА" };
            Thu = new Button { Text = "ЧЕТВЕРГ" };
            Fri = new Button { Text = "ПЯТНИЦА" };
            Sat = new Button { Text = "СУББОТА" };
            Update = new Button { Text = "Обновить расписание" };
            Change = new Button { Text = "Внести изменения" };

            Update.Clicked += Update_Clicked;
            Change.Clicked += Change_ClickedAsync;
            ScrollView scrollView = new ScrollView { Content = grid };
            // Build the page.
            stackLayout.Children.Add(scrollView);
            //OnAlertYesNoClicked(this, new EventArgs());
            
        }
        async void OnAlertYesNoClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Question?", "Обновить расписание", "Да", "Нет");
            
        }
        private async void Change_ClickedAsync(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new ChangesPage());
        }
        
        public async void Update_Clicked(object sender, EventArgs e)
        {
            grid.Children.Clear();
            var tt = DbService.LoadAllTimeTable();
            DbService.RemoveTimeTable(tt);
            List<TimeTable> timeTables = new List<TimeTable>();
            if (ClientControls.CurrentUser == "Преподаватель")
            {
                var teacher = DbService.LoadAllTeacher();
                var teachers = await new TeacherService().GetTeachers();
                int id = 0;
                var st = teacher[0].Full_Name.Split(' ');
                string name = st[0] + " " + st[1][0] + ". " + st[2][0] + ".";
                for (int i = 0; i < teachers.Count; i++)
                {
                    if (teachers[i].full_name == name)
                    {
                        id = teachers[i].id_t;
                        break;
                    }
                }
                timeTables = await new TimeTableService().GetTeacherTimeTable(id);

                
                DbService.AddTimeTable(timeTables);

                TimeTableData.TimeTables = timeTables;
            }
            else
            {
                var student = DbService.LoadAllStudent();
                timeTables = await new TimeTableService().GetStudentTimeTable(student[0].Group, student[0].Subgroup);
                DbService.AddTimeTable(timeTables);
                TimeTableData.TimeTables = timeTables;

            }
            DependencyService.Get<IToast>().Show("Изменения выполнены");
            
            picker_SelectedIndexChanged(this, new EventArgs());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //OnAlertYesNoClicked(this, new EventArgs());

        }

        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClientControls.CurrentUser == null || ClientControls.CurrentUser == "")
            {
                DependencyService.Get<IToast>().Show("Авторизируйтесь в системе");
                return;
            }

            if (TimeTableData.TimeTables.Count == 0) return;

            int x = 0; int y = 0;

            if (picker.SelectedIndex == -1)
            {
                if (Type == "")
                    DependencyService.Get<IToast>().Show("Ошибка");
            }
            else

            if ((picker.Items[picker.SelectedIndex] == "Числитель" || Type == "Числитель") && picker.Items[picker.SelectedIndex] != "Знаменатель")
            {
                Type = "Числитель";
                grid.Children.Clear();

                #region Понедельник
                grid.Children.Add(Mon, x + 1, y);
                var Day = from timatable in TimeTableData.TimeTables
                          where timatable.Parity == "Числитель"
                          where timatable.Week_day == "Понедельник"
                          select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        // HorizontalTextAlignment = TextAlignment.Center
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                    {

                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    
                    x++;
                    y++; x = 0;
                }
                #endregion

                #region Вторник
                grid.Children.Add(Tue, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Числитель"
                      where timatable.Week_day == "Вторник"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        // HorizontalTextAlignment = TextAlignment.Center
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);

                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Среда
                grid.Children.Add(Wed, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Числитель"
                      where timatable.Week_day == "Среда"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        //HorizontalTextAlignment = TextAlignment.Center
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Четверг
                grid.Children.Add(Thu, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Числитель"
                      where timatable.Week_day == "Четверг"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Пятница
                grid.Children.Add(Fri, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Числитель"
                      where timatable.Week_day == "Пятница"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Суббота
                grid.Children.Add(Sat, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Числитель"
                      where timatable.Week_day == "Суббота"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion

            }
            else
            if ((picker.Items[picker.SelectedIndex] == "Знаменатель" || Type == "Знаменатель") && picker.Items[picker.SelectedIndex] != "Числитель")
            {
                grid.Children.Clear();
                Type = "Знаменатель";
                #region Понедельник
                grid.Children.Add(Mon, x + 1, y);
                var Day = from timatable in TimeTableData.TimeTables
                          where timatable.Parity == "Знаменатель"
                          where timatable.Week_day == "Понедельник"
                          select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion

                #region Вторник
                grid.Children.Add(Tue, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Знаменатель"
                      where timatable.Week_day == "Вторник"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Среда
                grid.Children.Add(Wed, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Знаменатель"
                      where timatable.Week_day == "Среда"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Четверг
                grid.Children.Add(Thu, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Знаменатель"
                      where timatable.Week_day == "Четверг"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Пятница
                grid.Children.Add(Fri, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Знаменатель"
                      where timatable.Week_day == "Пятница"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Суббота
                grid.Children.Add(Sat, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Знаменатель"
                      where timatable.Week_day == "Суббота"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == 0)
                        grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);

                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion

            }
            grid.Children.Add(Update, x + 1, y);
            if (ClientControls.CurrentUser == "Преподаватель")
            {
                grid.Children.Add(Change, x + 1, y + 1);
            }
        }
    }
}