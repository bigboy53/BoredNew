using System;

namespace DKD.Core.Cache
{
    public interface ICacheProvider
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);
        /// <summary>
        /// 添加缓存(默认无过期时间)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key,object value);
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minutes">过期时间（分钟）</param>
        void Set(string key, object value, int minutes);
        void Set(string key, object value, int minutes, bool isAbsoluteExpiration,Action<string,object,string> onRemove);
        void Remove(string key);
        void Clear(string keyRegex);
    }
}
