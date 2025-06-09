using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Application.Core.Cache
{
    public interface ICacheService
    {
        ValueTask<T?> GetAsync<T>(string key);
        ValueTask SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    }
}
