#region using

using System;
using System.Runtime.Caching;

#endregion

namespace EM.Framework.Cache
{
    public static class CacheMgr
    {
        //TODO: Move the CacheStorage to Global Container.
        private static ICacheStorage _cacheStorage;

        public static TimeSpan DefaultCacheTimeSpan
        {
            get { return TimeSpan.FromMilliseconds(Constants.DefaultCachingTimeinMSec); }
        }

        public static DateTimeOffset DefaultCacheTimeOffSet
        {
            get { return DateTimeOffset.Now.AddMilliseconds(Constants.DefaultCachingTimeinMSec); }
        }

        public static ICacheStorage GetCacheStorage()
        {
            return _cacheStorage ?? (_cacheStorage = new MemoryCacheStorage());
        }

        public static void SetCacheStorage(ICacheStorage storage)
        {
            if (_cacheStorage != null)
                throw new Exception("Cache Storage is already set");
            _cacheStorage = storage;
        }

        public static CacheItemPolicy GetSlidingExpirationCacheItemPolicy(
            double valueinMSec = Constants.DefaultCachingTimeinMSec)
        {
            return new CacheItemPolicy {SlidingExpiration = TimeSpan.FromMilliseconds(valueinMSec)};
        }

        public static CacheItemPolicy GetAbsoluteExpirationCacheItemPolicy(
            double valueinMSec = Constants.DefaultCachingTimeinMSec)
        {
            return new CacheItemPolicy {AbsoluteExpiration = DateTimeOffset.Now.AddMilliseconds(valueinMSec)};
        }
    }
}