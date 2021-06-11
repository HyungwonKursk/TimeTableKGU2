using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Models;

namespace TimeTableKGU.Web.Services
{
    class PrivateLinkService
    {
        const string Url = WebData.ADRESS + "api/LessonsApi/";


        // получаем расписание для студента
        public async Task<bool> PutLink(int? id, Link _link)
        {
            // сериализация объекта с помощью Json.NET
            string json = JsonConvert.SerializeObject(_link);
            HttpContent content = new StringContent(json,
                                    Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            HttpResponseMessage result = await client.PutAsync(Url + id, content);
            if (result.StatusCode == HttpStatusCode.NoContent)
                return true;
            else
                return false;

        }
    }
}
