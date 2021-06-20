using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Interface;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public Label labelMessage { get; set; }
        public Label labelMessage2 { get; set; }
        public Label NameLab { get; set; }
        public Label TimeLab { get; set; }
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
            labelMessage2 = new Label
            {
                Style = Device.Styles.TitleStyle,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };
            labelText = new Label
            {
                FontSize = 18,
                Text="Выберите день недели из списка",
                Style = Device.Styles.TitleStyle,
                TextColor = Color.Black
            };

            NameBox = new Entry
            {
                Text = "",
                Placeholder="ФИО",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            NameLab = new Label
            {
                Text = "Введите ФИО преподавателя:",
                TextColor=Color.Black,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            TimeLab = new Label
            {
                Text = "",
                TextColor = Color.Black,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            TimeBox = new Entry
            {
                Text = "",
                Placeholder ="ЧЧ:ММ",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            DayPicker = new Picker {  Items = {"Понедельник","Вторник","Среда","Четверг","Пятница","Суббота" },TextColor = Color.Black, BackgroundColor = Color.FromHex("#4682B4") };

            FindBtn.Clicked += FindBtn_ClickedAsync;
            
        }

        private async void FindBtn_ClickedAsync(object sender, EventArgs e)
        {
            int K = TimeBox.Text.IndexOf('-');
            if (TimeBox.Text.IndexOf('-') != -1)
            {
                DependencyService.Get<IToast>().Show("Не корректно введено время. Корректный формат ЧЧ:ММ");
                return;
            }

            labelMessage.Text = "";
            if (picker.Items[picker.SelectedIndex] == "1. Поиск преподавателя по ФИО")
            {
                if (DayPicker.SelectedIndex == -1  || NameBox.Text == "")
                { DependencyService.Get<IToast>().Show("Не все поля заполнены"); return; }
                if (TimeBox.Text == "")
                    TimeBox.Text = "0";
                var teachers = await new TeacherService().GetTeachers();
                int id = 0;
                string name;
                try
                {
                    var st = NameBox.Text.Split(' ');
                     name = st[0] + " " + st[1][0] + ". " + st[2][0] + ".";
                }
                catch
                {
                    DependencyService.Get<IToast>().Show("Проверьте корректность ввода ФИО.");
                    return;
                }
                for (int i = 0; i < teachers.Count; i++)
                {
                    if (teachers[i].full_name == name)
                    {
                        id = teachers[i].id_t;
                        break;
                    }
                }
                if (id == 0)
                {
                    for (int i = 0; i < teachers.Count; i++)
                    {
                        if (teachers[i].full_name == NameBox.Text)
                        {
                            id = teachers[i].id_t;
                            break;
                        }
                    }
                    if (id == 0)
                    {
                        DependencyService.Get<IToast>().Show("Преподаватель не найден. Проверьте корректность ввода ФИО.");
                        return;
                    }
                }
                
                var rooms = await new TeacherService().SearchTeacher(id, DayPicker.Items[DayPicker.SelectedIndex], TimeBox.Text);
                labelMessage.Text = ""; labelMessage2.Text = "";
                if (rooms[0] == "Числитель: ") labelMessage.Text += "Числитель: Преподаватель на кафедре или его нет в университете";
                else labelMessage.Text += rooms[0];
                if (rooms[1] == "Знаменатель: ") labelMessage2.Text += "Знаменатель: Преподаватель на кафедре или его нет в университете";
                else labelMessage2.Text += rooms[1];

            }
            if (picker.Items[picker.SelectedIndex] == "2. Поиск свободной аудитории по времени")
            {
                if (DayPicker.SelectedIndex == -1 || TimeBox.Text == "")
                { DependencyService.Get<IToast>().Show("Не все поля заполнены"); return; }

                var rooms = await new RoomService().GetRoom(DayPicker.Items[DayPicker.SelectedIndex], TimeBox.Text);
                labelMessage.Text = rooms[0] + " " + rooms[1];
            }
        }

        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            header.Text = "Вы выбрали: " ;
            header.TextColor= Color.Black;
            if (picker.Items[picker.SelectedIndex] == "1. Поиск преподавателя по ФИО")
            {
                labelMessage.Text = "";
                labelMessage2.Text = "";
                stackLayout.Children.Remove(TimeLab);
                stackLayout.Children.Remove(FindBtn);
                stackLayout.Children.Remove(labelMessage2);
                stackLayout.Children.Remove(labelMessage);
                stackLayout.Children.Remove(TimeBox);
                stackLayout.Children.Remove(labelText);
                stackLayout.Children.Remove(DayPicker);

                NameBox.Text = "";
                TimeBox.Text = "";
                TimeLab.Text = "Введите время, если время не будет введено, то будет показа информация на весь день";

                stackLayout.Children.Add(NameLab);
                stackLayout.Children.Add(NameBox);
                stackLayout.Children.Add(TimeLab);
                stackLayout.Children.Add(TimeBox);
                stackLayout.Children.Add(labelText);
                stackLayout.Children.Add(DayPicker);
                


            }
            if (picker.Items[picker.SelectedIndex] == "2. Поиск свободной аудитории по времени")
            {
                labelMessage.Text = "";
                labelMessage2.Text = "";
                stackLayout.Children.Remove(NameLab);
                stackLayout.Children.Remove(FindBtn);
                stackLayout.Children.Remove(TimeLab);
                stackLayout.Children.Remove(labelMessage);
                stackLayout.Children.Remove(labelMessage2);
                stackLayout.Children.Remove(NameBox);
                stackLayout.Children.Remove(TimeBox);
                stackLayout.Children.Remove(labelText);
                stackLayout.Children.Remove(DayPicker);

                TimeLab.Text = "Введите время:";
                TimeBox.Text = "";
                stackLayout.Children.Add(TimeLab);
                stackLayout.Children.Add(TimeBox);
                stackLayout.Children.Add(labelText);
                stackLayout.Children.Add(DayPicker);

            }
            stackLayout.Children.Add(FindBtn);
            stackLayout.Children.Add(labelMessage);
            stackLayout.Children.Add(labelMessage2);
            stackLayout.Spacing = 6;
            this.Content = new ScrollView
            {
                Content = stackLayout
            };
            //this.Content = stackLayout;
        }
    }
}