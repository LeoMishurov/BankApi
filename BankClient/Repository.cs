using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BankClient.Model;
using Newtonsoft.Json;

namespace BankClient
{
    internal class Repository
    {
        //переменная для записи токена
        //private static string Token;

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
            client.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", GlobalVar.Token);
            return client;
        }

        /// <summary>
        /// преобразует передаваемый класс в параметры api
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
        /// <summary>
        /// авторизация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Authorization(string login, string password)
        {
            /// using закрывает поток после выполнения задачи
            using var client = GetClient();
            var response = await client.PostAsync($"api/User/token?username={login}&password={password}",
                null);

            if (response.IsSuccessStatusCode)
            {
                var authResult = await response.Content.ReadFromJsonAsync<AuthResult>();
                //Запись токена в переменную
               GlobalVar.Token = authResult.access_token;
                return true;
            }
            else
            {
                GlobalVar.Token = "";
                return false;
            }
        }
        /// <summary>
        /// пополнение баланса
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public async Task<Result> BalanceAdd(string sum, string cardNumber)
        {
            return await CallPost<string>($"api/Card/Balance?cardNumber={cardNumber}&sum={sum}");
        }

        /// <summary>
        /// оплата
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public async Task<Result> Pay(string sum, string cardNumber)
        {
            return await CallPostLight($"api/Card/Pay?cardNumber={cardNumber}&sum={sum}");
        }

        /// <summary>
        /// перевод по номеру карты
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public async Task<Result> Remittance(string sum, string fromCardNumber, string inCardNumber)
        {
            return await CallPostLight($"api/Card/Remittance?sum={sum}&fromCardNumber={fromCardNumber}&inCardNumber={inCardNumber}");
        }

        /// <summary>
        /// Установка дневного лимита
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public async Task<Result> DailyLimit(string sum, string cardNumber)
        {
            return await CallPost<string>($"api/Card/DailyLimit?cardNumber={cardNumber}&sum={sum}");
        }

        /// <summary>
        /// выпуск карты
        /// </summary>
        /// <returns></returns>
        public async Task<Result<CardDTO>> CardAdd()
        {
            return await CallPost<CardDTO>("api/Card/Save");
        }

        /// <summary>
        /// возвращает все карты польователя
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<CardDTO>>> ReturnCards()
        {
            return await CallPost<List<CardDTO>>("api/Card/ReturnCards");
        }

        /// <summary>
        /// блокировка карты
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public async Task<Result> Block(string cardNumber) 
        {
            return await CallPost<string>($"api/Card/Block?cardNumber={cardNumber}");            
        }

        /// <summary>
        /// разблокировка карты
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public async Task<Result> UnBlock(string cardNumber)
        {
            return await CallPost<string>($"api/Card/UnBlock?cardNumber={cardNumber}");
        }

        /// <summary>
        /// обработка ошибок при завпросе в api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<Result<T>> CallPost<T>(string url)
        {
            /// using закрывает поток после выполнения задачи
            using var client = GetClient();
            var response = await client.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
                return Result.Ok(await response.Content.ReadFromJsonAsync<T>());
            return Result.Fail<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// обработка ошибок при завпросе в api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<Result> CallPostLight(string url)
        {
            /// using закрывает поток после выполнения задачи
            using var client = GetClient();
            var response = await client.PostAsync(url, null);
            if (response.IsSuccessStatusCode)
                return Result.Ok();
            return Result.Fail(await response.Content.ReadAsStringAsync());
        }
    }
}
