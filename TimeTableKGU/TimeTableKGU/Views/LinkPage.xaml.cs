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
            int id = 0;
            var teachers = await new TeacherService().GetTeachers();
            var st = TeacherData.Teachers[0].Full_Name.Split(' ');
            string name = st[0] + " " + st[1][0] + ". " + st[2][0] + ".";
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
                    if (teachers[i].full_name == TeacherData.Teachers[0].Full_Name)
                    {
                        id = teachers[i].id_t;
                        break;
                    }
                }
                if (id == 0)
                {
                    DependencyService.Get<IToast>().Show("Что-то пошло не так");
                    return;
                }
            }
            var answer = await new PrivateLinkService().PutLink(id,new Link(LinkBox.Text, Convert.ToInt32(GroupBox.Text)));
            if (answer)
                DependencyService.Get<IToast>().Show("Ссылка добавлена");
            else
                DependencyService.Get<IToast>().Show("Произошла ошибка. Повторите запрос позже");
        }
    }
}