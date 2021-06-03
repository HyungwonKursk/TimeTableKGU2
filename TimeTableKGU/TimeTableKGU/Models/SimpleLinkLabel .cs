using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TimeTableKGU.Models
{
    public class SimpleLinkLabel : Label
    {
        public SimpleLinkLabel(Uri uri, string labelText = null)
        {
            Text = labelText ?? uri.ToString();
            TextColor = Color.Blue;
            GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => Device.OpenUri(uri)) });
        }
    }
}
