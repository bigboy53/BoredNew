using System;
using System.Web;
using log4net;

namespace DKD.Framework.Logger
{
    public class LoggerHelper
    {
        #region 错误日志

        private static readonly String ConfigPath = HttpContext.Current == null
            ? (AppDomain.CurrentDomain.BaseDirectory) + @"Config\log4netConfig.xml"
            : HttpContext.Current.Server.MapPath("~/Config/log4netConfig.xml");
        private static readonly ILog Log;

        static LoggerHelper()
        {
            var path = ConfigPath;
            if (!System.IO.File.Exists(path))
            {
                throw new Exception(string.Format("路径文件不正确：{0}", path));
            }
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
            Log = LogManager.GetLogger(typeof(Nullable));
        }

        /// <summary>
        /// 记录日志
        /// </summary> 
        public static void Logger(string title)
        {
            try
            {
                string msg = title;
                Log.Info(msg);
            }
            catch { }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        public static void Logger(string title, Exception ex)
        {
            try
            {
                var msg = String.Format("标题：{0},时间：{1},错误信息：{2}",title,DateTime.Now,ex.ToString());
                Log.Error(msg);
            }
            catch { }
        }

        #endregion
    }
}
