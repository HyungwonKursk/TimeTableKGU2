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
            }

        
        }
        public ChangeControls changeControls;
        public async void GetChangePage()
        {
            changeControls = new ChangeControls();
            bool isChange = false;
            for (int i = 0; i < TimeTableData.TimeTables.Count; i++)
            {
                if (TimeTableData.TimeTables[i].Subject == changeControls.nameBox.Text &&
                    TimeTableData.TimeTables[i].Time == changeControls.timeBox.Text)
                {
                    isChange = await new TimeTableService().ChangeRoom(TimeTableData.TimeTables[i].TimeTableId,
                        Convert.ToInt32(changeControls.roomBox.Text));
                    break;
                }
            }


        }
    }
}
