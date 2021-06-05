using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Data;
using TimeTableKGU.Interface;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangesPage : ContentPage
    {
        public Entry nameBox { get; set; }
        public Entry timeBox { get; set; }
        public Entry roomBox { get; set; }
        public Button changeBtn { get; set; }
        public Picker DayPicker { get; set; }
        public Label labelMessage { get; set; }
        public ChangesPage()
        {
            Title = "Внесение изменений в расписание";
            InitializeComponent();
            labelMessage = new Label
            {
                Text="Для отмены занятия введите аудиторию 0"
            };
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
            changeBtn = new Button { Text = "Изменить" };
            DayPicker = new Picker { Items = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" } };
            SetContent();
            changeBtn.Clicked += ChangeBtn_Clicked;
        }

        private async void ChangeBtn_Clicked(object sender, EventArgs e)
        {
            if (nameBox.Text == "" || timeBox.Text == "" || DayPicker.SelectedIndex==-1 || roomBox.Text=="")
            {
                DependencyService.Get<IToast>().Show("Не все поля заполнены"); return;
            }
            if (roomBox.Text != "0")
            {
                TimeTablePage tt = new TimeTablePage();
                bool isChange = false;
                for (int i = 0; i < TimeTableData.TimeTables.Count; i++)
                {
                    if (TimeTableData.TimeTables[i].Subject == nameBox.Text &&
                        TimeTableData.TimeTables[i].Time == timeBox.Text &&
                        TimeTableData.TimeTables[i].Week_day == DayPicker.Items[DayPicker.SelectedIndex] && 
                        TimeTableData.TimeTables[i].Parity == TimeTablePage.Type)
                    {
                        isChange = await new TimeTableService().ChangeRoom(TimeTableData.TimeTables[i].TimeTableId,
                            Convert.ToInt32(roomBox.Text));
                        break;
                    }
                }
                await Navigation.PopAsync(); 
                tt.Update_Clicked(tt, new EventArgs());
            }
            
            
                
        }

        public void SetContent()
        {
            stackLayout.Children.Add(nameBox);        
            stackLayout.Children.Add(timeBox);
            stackLayout.Children.Add(labelMessage);
            stackLayout.Children.Add(roomBox);
            stackLayout.Children.Add(DayPicker);
            stackLayout.Children.Add(changeBtn);

        }
    }
}