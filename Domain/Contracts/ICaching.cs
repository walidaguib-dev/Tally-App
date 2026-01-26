using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface ICaching
    {
        public Task<T?> GetFromCacheAsync<T>(string key);
        public Task SetAsync<T>(string key, T values , TimeSpan? expiry = null);
        public Task RemoveCaching(string key);
        public Task RemoveByPattern(string pattern);
    }
}
