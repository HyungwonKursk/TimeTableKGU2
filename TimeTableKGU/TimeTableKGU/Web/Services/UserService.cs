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
    class UserService
    {
        const string Url = WebData.ADRESS + "";

        public async Task<Student> RegisterStudent(string login, string pass, int group,
            int sub_group, string name)
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url + "registerapi/" + login + "/" + pass + "/" +
                group + "/" + sub_group + "/" + name);
            return JsonConvert.DeserializeObject<Student>(result);
        }
        public async Task<Student> AuthrizationStudent(string login, string pass)
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url + "registerapi/author/student/" + login + "/" + pass);
            return JsonConvert.DeserializeObject<Student>(result);
        }
        public async Task<Teacher> RegisterTeacher(string login, string pass, string pos,
            string depart, string name)
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url + "registerapi/regTeach/" + login + "/" + pass + "/" +
                pos + "/" + depart + "/" + name);
            return JsonConvert.DeserializeObject<Teacher>(result);
        }
        public async Task<Teacher> AuthrizationTeacher(string login, string pass)
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url + "registerapi/author/teacher/" + login + "/" + pass);
            return JsonConvert.DeserializeObject<Teacher>(result);
        }
    }
}
