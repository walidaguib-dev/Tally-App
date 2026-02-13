using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public interface IInvalidateCache
    {
        List<string> CacheKeys { get; }
    }
}
