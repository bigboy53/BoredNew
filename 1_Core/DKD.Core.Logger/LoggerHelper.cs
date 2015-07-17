using System;
using System.IO;
using System.Text;
using System.Web;
using log4net;

namespace DKD.Framework.Logger
{
    public class LoggerHelper
    {
        static LoggerHelper()
        {
            //初始化log4net配置
            var config = CachedConfigContext.Current.ConfigService.GetConfig("log4net");
            //重写log4net配置里的连接字符串
            config = config.Replace("{connectionString}", CachedConfigContext.Current.DaoConfig.Log);
            var ms = new MemoryStream(Encoding.Default.GetBytes(config));
            log4net.Config.XmlConfigurator.Configure(ms);
        }
    }
}
