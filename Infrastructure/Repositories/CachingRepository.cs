using Domain.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class CachingRepository(
        IConnectionMultiplexer connection
        ) : ICaching
    {
        private readonly IConnectionMultiplexer _connection = connection;
        public async Task<T?> GetFromCacheAsync<T>(string key)
        {
            var db = _connection.GetDatabase();
            var cachedData = await db.StringGetAsync(key);
            if(!cachedData.HasValue)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(cachedData.ToString(), new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
        }

        public async Task RemoveByPattern(string pattern)
        {
            var db = _connection.GetDatabase();
            var server = _connection.GetServer(_connection.GetEndPoints().First());
            var keys = server.Keys(pattern: pattern + "*");
            foreach (var key in keys)
            {
                await db.KeyDeleteAsync(key);
            }
        }

        public async Task RemoveCaching(string key)
        {
            var db = _connection.GetDatabase();
            await db.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T values , TimeSpan? expiry = null)
        {
            var db = _connection.GetDatabase();
            var serializedData = JsonConvert.SerializeObject(values, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            try
            {

                // var data = Encoding.UTF8.GetBytes(serializedData);
                await db.StringSetAsync(key, serializedData, expiry: expiry ?? TimeSpan.FromMinutes(4));
                
            }
            catch (Exception ex)
            {
                // Handle Redis operations errors appropriately
                Console.WriteLine($"Error setting cache: {ex.Message}");
            }
        }
    }
}
