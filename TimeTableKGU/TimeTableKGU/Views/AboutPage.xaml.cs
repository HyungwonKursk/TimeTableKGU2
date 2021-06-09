using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public ICommand TapCommand =>
                        new Command<string>(
                            (url) => Xamarin.Essentials.Launcher.CanOpenAsync(url));

        public ICommand HelpCommand => new Command<string>((url) => Device.OpenUri(new Uri(url)));

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}