using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TimeTableKGU.Web.Services
{
    class RoomService
    {
        const string Url = WebData.ADRESS + "roomsapi/";

        // получаем список групп
        public async Task<List<string>> GetRoom(string day, string time)
        {
            time = time.Replace(':', '_');
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url + day + "/" + time);
            return JsonConvert.DeserializeObject<List<string>>(result);
        }
    }
}
