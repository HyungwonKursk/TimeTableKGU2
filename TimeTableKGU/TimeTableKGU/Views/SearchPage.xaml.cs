using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public Label labelMessage { get; set; }
        public Entry NameBox { get; set; }
        public TimePicker timePicker { get; set; }
        public SearchPage()
        {
            InitializeComponent();
            labelMessage = new Label
            {
                Style = Device.Styles.TitleStyle,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };

            NameBox = new Entry
            {
                Text = "",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            timePicker = new TimePicker() { Time = new System.TimeSpan(17, 0, 0) };
            stackLayout.Children.Add(labelMessage);
        }
        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {


            

            header.Text = "Вы выбрали: " + picker.Items[picker.SelectedIndex];
            if (picker.Items[picker.SelectedIndex] == "Поиск преподавателя по ФИО")
            {
                stackLayout.Children.Remove(timePicker);
                labelMessage.Text = "Введите ФИО преподавателя";
                stackLayout.Children.Add(NameBox);
                
            }
            if (picker.Items[picker.SelectedIndex] == "Поиск свободной аудитории по времени")
            {
                stackLayout.Children.Remove(NameBox);
                labelMessage.Text = "Введите время занятия";
                stackLayout.Children.Add(timePicker);

            }
            //stackLayout.Children.Add(Btn);


            //this.Content = stackLayout;
        }
    }
}