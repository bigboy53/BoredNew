namespace DKD.Core.Config.Helper
{
    public class SitePathHelper
    {
        /// <summary>
        /// 应用根目录地址
        /// </summary>
        public static string WebPath
        {
            get
            {
                return System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
            }
        }

        /// <summary>
        /// 系统配置文件地址
        /// </summary>
        public static string SysConfigPath
        {
            get
            {
                return @"\Config\SysConfig.xml";
            }
        }
    }
}
