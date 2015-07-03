using System;
using System.Web;
using System.Web.Caching;

namespace DKD.Core.Cache
{
    /// <summary>
    /// 本地缓存帮助类
    /// </summary>
    public class Caching
    {
        /// <summary>
        /// 本地缓存获取
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        /// 本地缓存移除
        /// </summary>
        /// <param name="key">key</param>
        public static void Remove(string key)
        {
            if (HttpRuntime.Cache[key] != null)
                HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// 本地缓存写入（默认缓存20min）
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public static void Set(string key, object value)
        {
            Set(key, value, null);
        }

        /// <summary>
        /// 本地缓存写入（默认永久缓存）,依赖项
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="cacheDependency">依赖项</param>
        public static void Set(string key, object value, CacheDependency cacheDependency)
        {
            HttpRuntime.Cache.Insert(key, value, cacheDependency, System.Web.Caching.Cache.NoAbsoluteExpiration,
                System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 本地缓存写入
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="minutes">缓存分钟</param>
        public static void Set(string key, object value, int minutes)
        {
            HttpRuntime.Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes));
        }

        /// <summary>
        /// 本地缓存写入，包括分钟，是否绝对过期及缓存过期的回调
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="minutes">缓存分钟</param>
        /// <param name="isAbsoluteExpiration">是否绝对过期</param>
        /// <param name="onRemoveCallback">缓存过期回调</param>
        public static void Set(string key, object value, int minutes, bool isAbsoluteExpiration, CacheItemRemovedCallback onRemoveCallback)
        {
            if (isAbsoluteExpiration)
                HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(minutes), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, onRemoveCallback);
            else
                HttpRuntime.Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes), CacheItemPriority.Normal, onRemoveCallback);
        }
    }
}
