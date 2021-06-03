using System;
using System.Collections.Generic;
using System.Text;
using TimeTableKGU.Data;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;

namespace TimeTableKGU.Views
{
    public partial class TimeTablePage : ContentPage
    {
        public class ChangeControls
        {
            public Entry nameBox{get;set; }
            public Entry timeBox { get; set; }
            public Entry roomBox { get; set; }
            public Button changeBtn { get; set; }
            public Picker DayPicker { get; set; }

            public ChangeControls()
            {
                nameBox = new Entry
                {
                    Text = "",
                    Placeholder = "Название дисциплины",
                    Keyboard = Keyboard.Default,
                    TextColor = Color.Black,
                    PlaceholderColor = Color.Black,
                    ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                    Style = Device.Styles.BodyStyle,
                    HorizontalOptions = LayoutOptions.Fill
                };
                timeBox = new Entry
                {
                    Text = "",
                    Placeholder = "Время занятия",
                    Keyboard = Keyboard.Default,
                    TextColor = Color.Black,
                    PlaceholderColor = Color.Black,
                    ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                    Style = Device.Styles.BodyStyle,
                    HorizontalOptions = LayoutOptions.Fill
                };
                roomBox = new Entry
                {
                    Text = "",
                    Placeholder = "Новая аудитория",
                    Keyboard = Keyboard.Default,
                    TextColor = Color.Black,
                    PlaceholderColor = Color.Black,
                    ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                    Style = Device.Styles.BodyStyle,
                    HorizontalOptions = LayoutOptions.Fill
                };
                changeBtn = new Button { Text = "изменить" };
                DayPicker = new Picker { Items = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" } };

            }

        
        }
        public ChangeControls changeControls;
        public void GetChangePage()
        {
            changeControls = new ChangeControls();
            
            changeControls.changeBtn.Clicked += ChangeBtn_Clicked;
           StackLayout stackLayout = new StackLayout();
            stackLayout.Margin = 20;
            stackLayout.Children.Add(changeControls.nameBox);
            stackLayout.Children.Add(changeControls.timeBox);
            stackLayout.Children.Add(changeControls.roomBox);
            stackLayout.Children.Add(changeControls.DayPicker);
            stackLayout.Children.Add(changeControls.changeBtn);
            
            this.Content = new ScrollView { Content = stackLayout };
        }

        private async void ChangeBtn_Clicked(object sender, EventArgs e)
        {
            
            bool isChange = false;
            for (int i = 0; i < TimeTableData.TimeTables.Count; i++)
            {
                if (TimeTableData.TimeTables[i].Subject == changeControls.nameBox.Text &&
                    TimeTableData.TimeTables[i].Time == changeControls.timeBox.Text &&
                    TimeTableData.TimeTables[i].Week_day == changeControls.DayPicker.Items[changeControls.DayPicker.SelectedIndex])
                {
                    isChange = await new TimeTableService().ChangeRoom(TimeTableData.TimeTables[i].TimeTableId,
                        Convert.ToInt32(changeControls.roomBox.Text));
                    break;
                }
            }
            changeControls = null;
            
            TimeTablePage tt = new TimeTablePage();
            this.Content = tt.Content;

            Update_Clicked(this, new EventArgs());
           
        }
    }
}
