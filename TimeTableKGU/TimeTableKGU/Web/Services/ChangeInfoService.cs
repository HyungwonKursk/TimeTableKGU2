using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Models;

namespace TimeTableKGU.Web.Services
{
    class ChangeInfoService
    {
        const string Url = WebData.ADRESS + "";

        public async Task<Student> ChangeStudent(int? id,string login, string pass, int? group,
            int? sub_group, string name)
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url + "studentsapi/edit/" + id + "/" + login + "/" + pass + "/" +
                group + "/" + sub_group + "/" + name);
            return JsonConvert.DeserializeObject<Student>(result);
        }
       
        public async Task<Teacher> ChangeTeacher(int? id, string login, string pass, 
            string depart, string name)
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url + "teachersapi/edit/" + id + "/" + login + "/" + pass + "/" +
                 depart + "/" + name);
            return JsonConvert.DeserializeObject<Teacher>(result);
        }

        
    }
}
