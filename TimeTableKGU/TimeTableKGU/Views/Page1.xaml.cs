using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Data;
using TimeTableKGU.DataBase;
using TimeTableKGU.Interface;
using TimeTableKGU.Models;
using TimeTableKGU.Web;
using TimeTableKGU.Web.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableKGU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            Button bt = new Button
            { 
                Text="Запрос"
            };

            bt.Clicked += Bt_Clicked;
            st.Children.Add(bt);
        }

        private async void Bt_Clicked(object sender, EventArgs e)
        {
           
            bool connect = await WebData.CheckConnection();
            if (connect == false) return;

            List<TimeTable> timeTables = await new TimeTableService().GetStudentTimeTable(413, 1);
            
            DbService.AddTimeTable(timeTables);

            TimeTableData.TimeTables = DbService.LoadAllTimeTable();
            // lb.Text = st;
            // var result = "{\"Week_day\":\"понедельник\",\"Time\":\"12.05\",\"Subject\":\"ИС и БД\"," +
            //"\"Name_Group\":\"Бабкин\",\"Room_Number\": 244,\"Link\":\"ссылка\",\"Parity\":\"Числитель\",}";
            // var t = JsonConvert.DeserializeObject<TimeTable>(result);
            // string result = "{\"StudentId\":\"Login\",\"Password\":\"Group\",\"Subgroup\":\"motor\",\"tabID\":null}";
            //  var s = JsonConvert.DeserializeObject<Student>(result);
        }
    }
}