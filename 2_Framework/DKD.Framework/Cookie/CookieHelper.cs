using System;
using System.Web;

namespace DKD.Framework.Cookie
{
    /// <summary>
    /// cookie操作常用类
    /// </summary>
    public  class CookieHelper
    {
        /// <summary>
        /// 写入cookie
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="response">输出对象</param>
        /// <param name="key">cookie键</param>
        /// <param name="value">cookie值</param>
        /// <param name="day">过期天数</param>
        public static void WriteCookie(HttpRequest request, HttpResponse response, string key, string value,int day)
        {
            var cookiename = new HttpCookie(key);
            cookiename.Name = key;
            cookiename.Value = value;
            cookiename.Expires = DateTime.Now.AddDays(day);
            response.Cookies.Add(cookiename);
        }

        /// <summary>
        /// 获取指定的cookie
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="key">cookie键</param>
        /// <returns></returns>
        public static string GetCookie(HttpRequest request,string key)
        {
            if (request.Cookies[key] != null)
            {
                return request.Cookies[key].Value;
            }
            return string.Empty;
        }
    }
}
