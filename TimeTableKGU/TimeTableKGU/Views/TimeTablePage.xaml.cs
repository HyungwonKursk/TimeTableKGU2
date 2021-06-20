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
            bool changes = false;
            bool answer = false;

            if (ClientControls.CurrentUser == "") return;
            try
            {
                if (ClientControls.CurrentUser == "Студент")
                    changes = await new TimeTableService().GetChanges(StudentData.Students[0].StudentId, "S");
                else
                    changes = await new TimeTableService().GetChanges(TeacherData.Teachers[0].TeacherId, "T");
            }
            catch
            {
                return;
            }
            if (changes)
                answer = await DisplayAlert("Question?", "Были внесены изменения.Обновить расписание", "Да", "Нет");

            if (answer)
                Update_Clicked(this, new EventArgs());
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
                
                timeTables = await new TimeTableService().GetTeacherTimeTable(teacher[0].TeacherId);

                
                DbService.AddTimeTable(timeTables);

                TimeTableData.TimeTables = timeTables;
            }
            else
            {
                var student = DbService.LoadAllStudent();
                timeTables = await new TimeTableService().GetStudentTimeTable(student[0].Group, student[0].Subgroup,student[0].StudentId);
                DbService.AddTimeTable(timeTables);
                TimeTableData.TimeTables = timeTables;

            }
            DependencyService.Get<IToast>().Show("Изменения выполнены");
            
            picker_SelectedIndexChanged(this, new EventArgs());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ClientControls.CurrentUser == null || ClientControls.CurrentUser == "") 
                return;
            OnAlertYesNoClicked(this, new EventArgs());

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
                grid.Children.Add(new Label { Text="ПОНЕДЕЛЬНИК",FontAttributes=FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center , TextColor = Color.Black }, x + 1, y);
                var Day = from timatable in TimeTableData.TimeTables
                          where timatable.Parity == "Числитель"
                          where timatable.Week_day == "Понедельник"
                          select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center,TextColor=Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                    if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch 
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }
                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    
                    x++;
                    y++; x = 0;
                }
                #endregion

                #region Вторник
                grid.Children.Add(new Label { Text = "ВТОРНИК",  FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Числитель"
                      where timatable.Week_day == "Вторник"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black

                    }, x, y);
                    x++;
                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                    if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }
                       
                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);

                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Среда
                grid.Children.Add(new Label { Text = "СРЕДА", FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Числитель"
                      where timatable.Week_day == "Среда"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                     if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }
                       
                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Четверг
                grid.Children.Add(new Label { Text = "ЧЕТВЕРГ", FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Числитель"
                      where timatable.Week_day == "Четверг"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time ,HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                    if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }
                       
                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Пятница
                grid.Children.Add(new Label { Text = "ПЯТНИЦА", FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Числитель"
                      where timatable.Week_day == "Пятница"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                         if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }
                        
                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Суббота
                grid.Children.Add(new Label { Text = "СУББОТА", FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Числитель"
                      where timatable.Week_day == "Суббота"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                               if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }
                       
                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
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
                grid.Children.Add(new Label { Text = "ПОНЕДЕЛЬНИК", FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                var Day = from timatable in TimeTableData.TimeTables
                          where timatable.Parity == "Знаменатель"
                          where timatable.Week_day == "Понедельник"
                          select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group, 
                        TextColor = Color.Black
                    }, x, y);
                    x++;

                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                               if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }

                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion

                #region Вторник
                grid.Children.Add(new Label { Text = "ВТОРНИК", FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Знаменатель"
                      where timatable.Week_day == "Вторник"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black
                    }, x, y);
                    x++;

                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                               if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }

                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Среда
                grid.Children.Add(new Label { Text = "СРЕДА", FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Знаменатель"
                      where timatable.Week_day == "Среда"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black
                    }, x, y);
                    x++;

                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                               if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }

                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Четверг
                grid.Children.Add(new Label { Text = "ЧЕТВЕРГ", FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Знаменатель"
                      where timatable.Week_day == "Четверг"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black
                    }, x, y);
                    x++;

                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                               if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }

                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);

                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Пятница
                grid.Children.Add(new Label { Text = "ПЯТНИЦА", FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Знаменатель"
                      where timatable.Week_day == "Пятница"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time,HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black
                    }, x, y);
                    x++;

                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                               if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }

                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);

                    x++;
                    y++; x = 0;
                }
                #endregion
                #region Суббота
                grid.Children.Add(new Label { Text = "СУББОТА", FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x + 1, y);
                Day = from timatable in TimeTableData.TimeTables
                      where timatable.Parity == "Знаменатель"
                      where timatable.Week_day == "Суббота"
                      select timatable;
                y++;
                foreach (TimeTable timetables in Day)
                {
                    grid.Children.Add(new Label { Text = timetables.Time, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    grid.Children.Add(new Label
                    {
                        Text = timetables.Subject + " " +
                        timetables.Name_Group,
                        TextColor = Color.Black
                    }, x, y);
                    x++;
                    if (timetables.Room_Number == -1)
                        grid.Children.Add(new Label { Text = "Занятие отменено", HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    else

                               if (timetables.Room_Number == 0)
                    {
                        try
                        {
                            grid.Children.Add(new SimpleLinkLabel(new Uri(Convert.ToString(timetables.Link))), x, y);
                        }
                        catch
                        {
                            grid.Children.Add(new Label { Text = timetables.Link, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                        }

                    }
                    else
                        grid.Children.Add(new Label { Text = Convert.ToString(timetables.Room_Number), HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Black }, x, y);
                    x++;
                    y++; x = 0;
                }
                #endregion

            }
            grid.Children.Add(Update, x + 1, y);
            if (ClientControls.CurrentUser == "Преподаватель" || StudentData.Students[0].Group_Leader)
            {
                grid.Children.Add(Change, x + 1, y + 1);
            }
        }
    }
}