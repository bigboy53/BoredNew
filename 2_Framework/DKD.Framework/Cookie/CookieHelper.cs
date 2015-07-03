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
        /// <param name="Request">请求对象</param>
        /// <param name="Response">输出对象</param>
        /// <param name="key">cookie键</param>
        /// <param name="value">cookie值</param>
        /// <param name="day">过期天数</param>
        public static void WriteCookie(HttpRequest Request, HttpResponse Response, string key, string value,int day)
        {
            HttpCookie cookiename = new HttpCookie(key);
            cookiename.Name = key;
            cookiename.Value = value;
            cookiename.Expires = DateTime.Now.AddDays(day);
            Response.Cookies.Add(cookiename);
        }

        /// <summary>
        /// 获取指定的cookie
        /// </summary>
        /// <param name="Request">请求对象</param>
        /// <param name="key">cookie键</param>
        /// <returns></returns>
        public static string GetCookie(HttpRequest Request,string key)
        {
            if (Request.Cookies[key] != null)
            {
                return Request.Cookies[key].Value;
            }
            return "-1";
        }
    }
}
