﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Data;
using TimeTableKGU.Interface;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TimeTableKGU.Views.AuthorizationPage;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangesPage : ContentPage
    {
        public Entry nameBox { get; set; }
        public Entry timeBox { get; set; }
        public Entry  teacherBox{ get; set; }
        public Entry roomBox { get; set; }
        public Button changeBtn { get; set; }
        public Picker DayPicker { get; set; }
        public Label labelMessage { get; set; }
        public ChangesPage()
        {
            Title = "Внесение изменений в расписание";
            InitializeComponent();
            labelMessage = new Label
            {
                Text="Для отмены занятия введите аудиторию 0"
            };
            nameBox = new Entry
            {
                Text = "",
                Placeholder = "Название дисциплины",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            timeBox = new Entry
            {
                Text = "",
                Placeholder = "Время занятия",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            teacherBox = new Entry
            {
                Text = "",
                Placeholder = "ФИО преподавателя как в расписании",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            roomBox = new Entry
            {
                Text = "",
                Placeholder = "Новая аудитория",
                Keyboard = Keyboard.Default,
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                Style = Device.Styles.BodyStyle,
                HorizontalOptions = LayoutOptions.Fill
            };
            changeBtn = new Button { Text = "Изменить" };
            DayPicker = new Picker { Items = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" } };
            SetContent();
            changeBtn.Clicked += ChangeBtn_Clicked;
        }

        private async void ChangeBtn_Clicked(object sender, EventArgs e)
        {
            int room;
            
            if (nameBox.Text == "" || timeBox.Text == "" || DayPicker.SelectedIndex==-1 || roomBox.Text=="")
            {
                DependencyService.Get<IToast>().Show("Не все поля заполнены"); return;
            }

            if (ClientControls.CurrentUser != "Преподаватель" && roomBox.Text == "0")
            {
                DependencyService.Get<IToast>().Show("У Вас нет прав для отмены занятия"); 
                return;
            }

            if (roomBox.Text == "0")
                room = -1;
            else
                room = Convert.ToInt32(roomBox.Text);

            TimeTablePage tt = new TimeTablePage();
            bool isChange = false;

            for (int i = 0; i < TimeTableData.TimeTables.Count; i++)
            {
                if (TimeTableData.TimeTables[i].Subject == nameBox.Text &&
                    TimeTableData.TimeTables[i].Time == timeBox.Text &&
                    TimeTableData.TimeTables[i].Week_day == DayPicker.Items[DayPicker.SelectedIndex] &&
                    TimeTableData.TimeTables[i].Parity == TimeTablePage.Type)
                {
                    isChange = await new TimeTableService().ChangeRoom(TimeTableData.TimeTables[i].TimeTableId,
                        room);
                    break;
                }
            }
            if (isChange)
            {
                await Navigation.PopAsync();
                tt.Update_Clicked(tt, new EventArgs());
            }
            else
                DependencyService.Get<IToast>().Show("Что-то пошло не так, изменение не были внесены.");


        }

        public void SetContent()
        {
            stackLayout.Children.Add(nameBox);        
            stackLayout.Children.Add(timeBox);
            stackLayout.Children.Add(labelMessage);
            stackLayout.Children.Add(roomBox);
            stackLayout.Children.Add(DayPicker);

            if (ClientControls.CurrentUser=="Студент")
                stackLayout.Children.Add(teacherBox);
          
             stackLayout.Children.Add(changeBtn);
        }
    }
}