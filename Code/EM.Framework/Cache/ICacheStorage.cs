#region using

using System;
using System.Collections.Generic;
using System.Runtime.Caching;

#endregion

namespace EM.Framework.Cache
{
    public interface ICacheStorage : IDisposable
    {
        T Get<T>(string key, string regionName = null) where T : class;
        object Get(string key, string regionName = null);

        T AddOrGetExisting<T>(string key, Func<T> createCache, CacheItemPolicy cacheItemPolicy, string regionName = null)
            where T : class;

        object AddOrGetExisting(string key, object o, CacheItemPolicy cacheItemPolicy, string regionName = null);

        void Add(string key, object o, CacheItemPolicy cacheItemPolicy, string regionName = null);

        object Remove(string key, string regionName = null);
        void Clear(string regionName = null);

        bool Contains(string key, string regionName = null);
        long GetCount(string regionName);
        IEnumerable<string> Keys { get; }
    }
}