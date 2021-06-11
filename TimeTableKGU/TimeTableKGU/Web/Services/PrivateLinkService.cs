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
        public async Task<HttpStatusCode> PutLink(int? id, Link _link)
        {
            // сериализация объекта с помощью Json.NET
            string json = JsonConvert.SerializeObject(_link);
            HttpContent content = new StringContent(json,
                                    Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            string s = Url + id;
            HttpResponseMessage response = await client.PutAsync(Url + id, content);
            return 0;

        }
    }
}
