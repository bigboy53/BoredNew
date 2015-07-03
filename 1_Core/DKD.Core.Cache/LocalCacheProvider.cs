using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace DKD.Core.Cache
{
    public class LocalCacheProvider:ICacheProvider
    {
        public object Get(string key)
        {
            return Caching.Get(key);
        }

        public void Set(string key, object value, int minutes, bool isAbsoluteExpiration, Action<string, object, string> onRemove)
        {
            Caching.Set(key, value, minutes,isAbsoluteExpiration, (k, v, reason) =>
            {
                if (onRemove != null)
                    onRemove(k, v, reason.ToString());
            });
        }

        public void Remove(string key)
        {
            Caching.Remove(key);
        }

        public void Clear(string keyRegex)
        {
            var keys = new List<string>();
            var enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var key = enumerator.Key.ToString();
                if (Regex.IsMatch(key, keyRegex, RegexOptions.IgnoreCase))
                    keys.Add(key);
            }

            for (int i = 0; i < keys.Count; i++)
            {
                HttpRuntime.Cache.Remove(keys[i]);
            }
        }

        public void Set(string key, object value)
        {
            Caching.Set(key,value);
        }

        public void Set(string key, object value, int minutes)
        {
            Caching.Set(key, value, minutes);
        }
    }
}
