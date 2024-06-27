using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BankApi
{
    public static class RedisHelper
    {
        //private readonly ConnectionMultiplexer _redis;
        //private readonly IDatabase _db;

        //public RedisHelper(string connectionString)
        //{
        //    // Подключение к Redis
        //    _redis = ConnectionMultiplexer.Connect(connectionString);
        //    _db = _redis.GetDatabase();
        //}

        //public RedisHelper()
        //{
        //    // Подключение к Redis
        //    _redis = ConnectionMultiplexer.Connect("localhost:6379");
        //    _db = _redis.GetDatabase();
        //}

        // Метод для создания объекта
        public static void SetObject<T>(this IDistributedCache cache, string key, List<T> obj, TimeSpan expiration = default)
        {
            //Если время жизни обьекта не задано
            if (expiration == default)
            {
                expiration = TimeSpan.MaxValue; // Установка времени жизни объекта на бесконечное
            }

            var json = JsonConvert.SerializeObject(obj);
            cache.SetString(key, json, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration});
        }

        // Метод для получения объекта по ключу
        public static List<T> GetObject<T>(this IDistributedCache cache, string key)
        {
            var json = cache.GetString(key);
            return string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<List<T>>(json);
        }

        // Метод для удаления объекта по ключу
        public static void RemoveObject(this IDistributedCache cache, string key)
        {
           cache.Remove(key);
        }
    }
}
