using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankClient
{
    internal class Repository
    {
        /// <summary>
        /// создание  HttpClient
        /// </summary>
        /// <returns></returns>
        private HttpClient GetClient() 
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7057/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        /// <summary>
        /// вреобразует передаваемый класс в параметры api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private StringContent ConvertToStringContent<T>(T data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// регистрация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Registration(string login, string password) 
        {
            /// using закрывает поток после выполнения задачи
            using var client = GetClient();
            var response = await client.PostAsync($"api/User/Add?login={login}&password={password}", 
                null);

            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> Authorization(string login, string password)
        {
            /// using закрывает поток после выполнения задачи
            using var client = GetClient();
            var response = await client.PostAsync($"api/User/token?username={login}&password={password}",
                null);

            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
