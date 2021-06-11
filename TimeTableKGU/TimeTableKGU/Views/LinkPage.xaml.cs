using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Models;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinkPage : ContentPage
    {
        public Button button { get; set; }
        public LinkPage()
        {
            InitializeComponent();
            button = new Button { Text="Отправить"};
            button.Clicked += Button_Clicked;
            StackLayout st = new StackLayout();
            st.Children.Add(button);
            this.Content = st;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var answer = await new PrivateLinkService().PutLink(2,new Link("https://discord.gg/gQZ3PCdW", 413));
        }
    }
}