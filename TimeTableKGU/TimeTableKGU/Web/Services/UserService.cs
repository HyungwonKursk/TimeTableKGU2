using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableKGU.Web.Services
{
    class UserService
    {
        const string Url = WebData.ADRESS + "";

        /// <summary>
        /// Запрос на авторизацию пользователя в системе
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="pass"> Пароль</param>
        /// <returns>Id,Login,Name пользователя в случае верных данных, иначе NULL</returns>
        public async Task<Dictionary<string, string>> Authrization(string login, string pass)
        {
            Dictionary<string, string> user = new Dictionary<string, string>();
            user.Add("Login", login);
            user.Add("Pass", pass);

            HttpClient client = WebData.GetClient();

            HttpResponseMessage response = await client.PostAsync(Url + "/PostAuthorization",
                                new StringContent(
                                    JsonConvert.SerializeObject(user),
                                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(
               await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Запрос системе на регистрацию нового пользователя
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="login">Логин </param>
        /// <param name="pass">Пароль</param>
        /// <returns>Id,Login,Name пользователя в случае верных данных, иначе NULL</returns>
        public async Task<Dictionary<string, string>> RegisterStudent( string login, string pass,int group,
            int sub_group, string name)
        {
            Dictionary<string, string> user = new Dictionary<string, string>();

            user.Add("Login", login);
            user.Add("Pass", pass);
            user.Add("Group", Convert.ToString(group));
            user.Add("SubGr", Convert.ToString(sub_group));
            user.Add("Name", name);


            HttpClient client = WebData.GetClient();
            HttpResponseMessage response = await client.PostAsync(Url + "/PostRegister",
                                new StringContent(
                                    JsonConvert.SerializeObject(user),
                                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
