using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public void Add(string key, object data, int duration)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            var byteData = Encoding.UTF8.GetBytes(serializedData);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(duration)
            };

            _distributedCache.Set(key, byteData, options);
        }

        public T Get<T>(string key)
        {
            T returnData = default(T);

            byte[] cacheData = _distributedCache.Get(key);
            if (cacheData != null)
            {
                var serializedData = Encoding.UTF8.GetString(cacheData);
                returnData = JsonConvert.DeserializeObject<T>(serializedData);
            }

            return returnData;
        }

        public object Get(string key)
        {
            return _distributedCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            byte[] cachedData = _distributedCache.Get(key);
            return cachedData != null;
        }

        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_distributedCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _distributedCache.Remove(JsonConvert.SerializeObject(key));
            }
        }
    }
}
