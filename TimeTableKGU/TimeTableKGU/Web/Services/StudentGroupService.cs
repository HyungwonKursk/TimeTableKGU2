using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Models;

namespace TimeTableKGU.Web.Services
{
    class StudentGroupService
    {
        const string Url = WebData.ADRESS + "api/";

        // получаем расписание для студента
        public async Task<List<Student>> GetStudentTimeTable(int group)
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url + group);
            return JsonConvert.DeserializeObject<List<Student>>(result);
        }
    }
}
