using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace TimeTableKGU.Web.Services
{
    class CheckService
    {
        const string Url = WebData.ADRESS + "registerapi/check/";
        // проверка занят ли данный логин
        public async Task<bool> GetCheckLogin(string login)
        {
            HttpClient client = WebData.GetClient();
            string result = await client.GetStringAsync(Url + login);
            return JsonConvert.DeserializeObject<bool>(result);
        }
    }
}
