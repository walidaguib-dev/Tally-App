using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface ICaching
    {
        public Task<T?> GetOrSetAsync<T>(string key,
             Func<CancellationToken, Task<T>> factory,
             TimeSpan? expiry = null);
        public Task RemoveCaching(string key);
    }
}
