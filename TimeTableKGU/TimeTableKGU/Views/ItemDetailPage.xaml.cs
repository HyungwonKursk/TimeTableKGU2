using System.ComponentModel;
using TimeTableKGU.ViewModels;
using Xamarin.Forms;

namespace TimeTableKGU.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}