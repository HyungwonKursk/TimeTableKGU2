using System;
using System.Collections.Generic;
using System.ComponentModel;
using TimeTableKGU.Models;
using TimeTableKGU.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableKGU.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}