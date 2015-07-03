using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DKD.Framework.StaticFile
{
    public static class StaticFileHelper
    {
        /// <summary>
        /// 取得静态服务器的网址
        /// 如果是https网站，跨域调用静态资源需要欺骗浏览器如：http://content..../.png 改成 //content..../.png
        /// </summary>
        /// <returns></returns>
        private static string _staticServiceUri = "";

        public static string GetStaticServiceUri()
        {
            if (_staticServiceUri==null)
            {
                _staticServiceUri = "http://"+HttpContext.Current.Request.Url.Authority;
            }
            return _staticServiceUri;
        }

        /// <summary>
        /// 得到静态文件
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string StaticFile(this UrlHelper helper, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return "";
            if (path.StartsWith("~"))
                return helper.Content(path);
            return GetStaticServiceUri() + path;
        }
        

        /// <summary>
        /// 得到图片文件，以及缩略图
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="path"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ImageFile(this UrlHelper helper, string path, string size = null)
        {
            if (string.IsNullOrEmpty(path))
                return helper.StaticFile(@"/content/images/no_picture.jpg");

            if (size == null)
                return helper.StaticFile(path);

            var ext = path.Substring(path.LastIndexOf('.'));
            var head = path.Substring(0, path.LastIndexOf('.'));
            var url = string.Format("{0}{1}_{2}{3}", GetStaticServiceUri(), head, size, ext);
            return url;
        }


        /// <summary>
        /// 得到文件服务器根网址
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string StaticFile(this UrlHelper helper)
        {
            return GetStaticServiceUri();
        }
    }
}
