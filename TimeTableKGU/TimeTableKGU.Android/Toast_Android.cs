using Android.Widget;
using TimeTableKGU.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]

namespace TimeTableKGU.Droid
{
    public class Toast_Android : Interface.IToast
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();

        }
    }
}