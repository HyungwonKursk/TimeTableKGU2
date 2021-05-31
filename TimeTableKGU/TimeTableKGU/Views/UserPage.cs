using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TimeTableKGU.Views
{
    public partial class Authorization : ContentPage
    {
        public class ClientControls
        {
            public Label NameLab { get; set; }
            public Label LoginLab { get; set; }

            public Label NoteCount { get; set; }

            public List<Editor> Editors { get; set; }
            public List<Switch> Switches { get; set; }
            public List<Label> Dates { get; set; }

            public Button LoginOutBtn { get; set; }

            public ClientControls()
            {
                NameLab = new Label
                {
                    //Text = Client.CurrentClient.Name,
                    Style = Device.Styles.TitleStyle,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                LoginLab = new Label
                {
                    //Text = Client.CurrentClient.Login,
                    Style = Device.Styles.TitleStyle,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                NoteCount = new Label
                {
                    //Text = "У вас " + Client.CurrentClient.Notes.Count + " заметок и " + Client.CurrentClient.Favorites.Count + " избраных",
                    Style = Device.Styles.ListItemTextStyle,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                LoginOutBtn = new Button
                {
                    Text = "Login Out",
                    BackgroundColor = Color.FromHex("#BFD9B6"),
                    TextColor = Color.Black,
                    BorderColor = Color.Black,
                };

                Editors = new List<Editor>();
                Switches = new List<Switch>();
                Dates = new List<Label>();
            }
        }
    }
}
