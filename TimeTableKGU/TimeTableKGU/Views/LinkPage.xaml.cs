using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Interface;
using TimeTableKGU.Data;
using TimeTableKGU.Models;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TimeTableKGU.DataBase;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinkPage : ContentPage
    {
        public Button SendBtn { get; set; }
        public Entry LinkBox { get; set; }
        public Entry GroupBox { get; set; }
        public LinkPage()
        {
            InitializeComponent();
            Title = "Прикрепить личную ссылку";
            SendBtn = new Button { Text="Отправить"};
            SendBtn.Clicked += Button_Clicked;
            LinkBox = new Entry
            {
                Text = "",
                Placeholder = "Введите ссылку для занятий",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            GroupBox = new Entry
            {
                Text = "",
                Placeholder = "Введите номер группы",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            StackLayout st = new StackLayout();
            st.Children.Add(LinkBox);
            st.Children.Add(GroupBox);
            st.Children.Add(SendBtn);
            this.Content = st;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var teachers = DbService.LoadAllTeacher();
                var answer = await new PrivateLinkService().PutLink(teachers[0].TeacherId, new Link(LinkBox.Text, Convert.ToInt32(GroupBox.Text)));
                if (answer)
                    DependencyService.Get<IToast>().Show("Ссылка добавлена");
                else
                    DependencyService.Get<IToast>().Show("Произошла ошибка. Повторите запрос позже");
            }
            catch
            {
                DependencyService.Get<IToast>().Show("Произошла ошибка. Возможно в базе нет расписания группы. Повторите запрос позже");
            }
        }
    }
}