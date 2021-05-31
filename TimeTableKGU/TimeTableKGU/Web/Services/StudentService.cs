using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableKGU.Web.Services
{
    class StudentService
    {
        const string Url = WebData.ADRESS + "studentsapi/";

        // получаем список групп
        public async Task<List<string>> GetStudents(int? group)
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url + group);
            return JsonConvert.DeserializeObject<List<string>>(result);
        }
    }
}
