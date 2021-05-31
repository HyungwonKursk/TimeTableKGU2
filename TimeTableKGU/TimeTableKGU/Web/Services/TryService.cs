using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Models;

namespace TimeTableKGU.Web.Services
{
    class TryService
    {
        const string Url = WebData.ADRESS + "api/Test";

        // получаем расписание для студента
        public async Task<string> GetTry(int c)
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url+"/"+c);
            return result;
        }
    }
}
