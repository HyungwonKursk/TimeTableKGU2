using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTableKGU.Models;

namespace TimeTableKGU.Web.Services
{
    class GroupService
    {
        const string Url = WebData.ADRESS + "api/";

        // получаем список групп
        public async Task<List<Group>> GetNumbersOfGroups()
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<List<Group>>(result);
        }
    }
}
