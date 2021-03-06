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
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (ClientControls.CurrentUser == null || ClientControls.CurrentUser == "")
                GetLoginPage();
            else
                GetClientPage();

        }
    }
}