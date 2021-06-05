using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Models;

namespace TimeTableKGU.Web.Services
{
    class TeacherService
    {
        const string Url = WebData.ADRESS + "";

        public async Task<List<TeacherBD>> GetTeachers()
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url+"api/TeachersApi");
            return JsonConvert.DeserializeObject<List<TeacherBD>>(result);
        }
        public async Task<List<string>> SearchTeacher(int id,string day, string time)
        {
            time = time.Replace(':', '_');
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url+ "teachersapi/" + id+"/"+day+"/"+time);
            return JsonConvert.DeserializeObject<List<string>>(result);
        }
    }
}
