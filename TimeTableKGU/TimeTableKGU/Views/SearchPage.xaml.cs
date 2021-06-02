using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public Label labelMessage { get; set; }
        public Label labelText{ get; set; }
        public Entry NameBox { get; set; }
        public Entry TimeBox { get; set; }
        public Picker DayPicker { get; set; }
        public Button FindBtn { get; set; }
        public SearchPage()
        {
            InitializeComponent();
            FindBtn = new Button
            {
                Text = "Поиск",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };
            labelMessage = new Label
            {
                Style = Device.Styles.TitleStyle,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };
            labelText = new Label
            {
                Text="Выберите день недели из списка",
                Style = Device.Styles.TitleStyle,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };

            NameBox = new Entry
            {
                Text = "",
                Placeholder="Введите ФИО преподавателя",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            TimeBox = new Entry
            {
                Text = "",
                Placeholder ="Введите время занятия",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            DayPicker = new Picker { Items = {"Понедельник","Вторник","Среда","Четверг","Пятница","Суббота" } };

            FindBtn.Clicked += FindBtn_ClickedAsync;
            
        }

        private async void FindBtn_ClickedAsync(object sender, EventArgs e)
        {
            labelMessage.Text = "";
            if (picker.Items[picker.SelectedIndex] == "Поиск преподавателя по ФИО")
            {
                
               var teachers = await new TeacherService().GetTeachers();
                int id = 0;
                var st = NameBox.Text.Split(' ');
                string name = st[0] + " " + st[1][0] + ". " + st[2][0] + ".";
                for (int i = 0; i < teachers.Count; i++)
                {
                    if (teachers[i].full_name == name)
                    {
                        id = teachers[i].id_t;
                        break;
                    }
                }
                var rooms = await new TeacherService().SearchTeacher(id, DayPicker.Items[picker.SelectedIndex]);
                if (rooms[0] == 0) labelMessage.Text = "Преподаватель на кафедре или его нет в университете";
                else
                {
                    labelMessage.Text = "аудитории: ";
                    for (int i = 0; i < rooms.Count; i++)
                    {
                        labelMessage.Text = Convert.ToString(rooms[i]) + " ";
                    }
                }
            }
            if (picker.Items[picker.SelectedIndex] == "Поиск свободной аудитории по времени")
            {
                var rooms = await new RoomService().GetRoom(DayPicker.Items[picker.SelectedIndex], TimeBox.Text);
                labelMessage.Text = rooms[0] + " " + rooms[1];
            }
        }

        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            header.Text = "Вы выбрали: " ;
            if (picker.Items[picker.SelectedIndex] == "Поиск преподавателя по ФИО")
            {
                labelMessage.Text = "";
                stackLayout.Children.Remove(FindBtn);
                stackLayout.Children.Remove(labelMessage);
                stackLayout.Children.Remove(TimeBox);
                stackLayout.Children.Remove(labelText);
                stackLayout.Children.Remove(DayPicker);
                //labelMessage.Text = "Введите ФИО преподавателя";

                stackLayout.Children.Add(NameBox);
                stackLayout.Children.Add(labelText);
                stackLayout.Children.Add(DayPicker);

            }
            if (picker.Items[picker.SelectedIndex] == "Поиск свободной аудитории по времени")
            {
                labelMessage.Text = "";
                stackLayout.Children.Remove(FindBtn);
                stackLayout.Children.Remove(labelMessage);
                stackLayout.Children.Remove(NameBox);
                stackLayout.Children.Remove(labelText);
                stackLayout.Children.Remove(DayPicker);
                stackLayout.Children.Add(TimeBox);
                stackLayout.Children.Add(labelText);
                stackLayout.Children.Add(DayPicker);

            }
            stackLayout.Children.Add(FindBtn);
            stackLayout.Children.Add(labelMessage);
            this.Content = new ScrollView
            {
                Content = stackLayout
            };
            //this.Content = stackLayout;
        }
    }
}