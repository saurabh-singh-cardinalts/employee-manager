#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

#endregion

namespace EM.Framework.Cache
{
    public class MemoryCacheStorage : ICacheStorage
    {
        private bool _isDisposed;

        private readonly MemoryCache _cache;

        private readonly Object _sync = new object();

        public MemoryCacheStorage()
        {
            _cache = MemoryCache.Default;
        }

        public MemoryCacheStorage(string name)
        {
            _cache = new MemoryCache(name);
        }

        public T Get<T>(string key, string regionName = null) where T : class
        {
            var result = _cache.Get(key, regionName) as T;
            return result;
        }

        public object Get(string key, string regionName = null)
        {
            return _cache.Get(key, regionName);
        }

       

        public T AddOrGetExisting<T>(string key, Func<T> createCache, CacheItemPolicy cacheItemPolicy, string regionName = null) where T : class
        {
            var result = Get<T>(key, regionName);
            if (result != null)
            {
                return result;
            }

            if (createCache != null)
            {
                result = createCache();
                if (result != null)
                {
                    Add(key, result, cacheItemPolicy, regionName);
                }
                else
                {
                    result = Remove(key, regionName) as T;
                }
            }

            return result;
        }

        //public T AddOrGetExisting<T>(string key, Func<T> createCache, CacheItemPolicy cacheItemPolicy,
        //                             string regionName = null) where T : class
        //{
        //    var result = Get<T>(key, regionName);
        //    if (result != null)
        //    {
        //        return result;
        //    }

        //    if (createCache != null)
        //    {
        //        result = createCache();
        //        if (result != null)
        //        {
        //            Add(key, result, cacheItemPolicy, regionName);
        //        }
        //        else
        //        {
        //            result = Remove(key, regionName) as T;
        //        }
        //    }

        //    return result;
        //}

        public object AddOrGetExisting(string key, object o, CacheItemPolicy cacheItemPolicy, string regionName = null)
        {
            var result = Get(key, regionName);
            if (result != null)
            {
                return result;
            }

            if (o != null)
            {
                Add(key, o, cacheItemPolicy, regionName);
            }
            else
            {
                o = Remove(key, regionName);
            }
            return o;
        }

        public void Add(string key, object o, CacheItemPolicy cacheItemPolicy, string regionName = null)
        {
            lock (_sync)
            {
                _cache.Add(key, o, cacheItemPolicy, regionName);
            }
        }
       
        public object Remove(string key, string regionName = null)
        {
            lock (_sync)
            {
                return _cache.Remove(key, regionName);
            }
        }

        public void Clear(string regionName = null)
        {
            var cacheKeys = Keys.ToList();
            foreach (var cacheKey in cacheKeys)
            {
                _cache.Remove(cacheKey, regionName);
            }
        }

        public bool Contains(string key, string regionName = null)
        {
            return _cache.Contains(key, regionName);
        }

        public long GetCount(string regionName)
        {
            return _cache.GetCount(regionName);
        }

        public IEnumerable<string> Keys
        {
            get { return _cache.Select(item => item.Key); }
        }

        ~MemoryCacheStorage()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                if (_cache != null)
                {
                    _cache.Dispose();
                }
            }
            _isDisposed = true;
        }
    }
}