using DKD.Core.Config;
using DKD.Core.Config.Models;

namespace DKD.Framework.View
{
    /// <summary>
    /// View路径配置信息类
    /// </summary>
    public class ViewPathHelper
    {
        #region 属性

        /// <summary>
        /// 配置缓存对像
        /// </summary>
        public FrameworkConfig CACHEOBJECT = null;

        /// <summary>
        /// 管理View路径
        /// </summary>
        public string MANAGEVIEWPATH = "";

        /// <summary>
        /// 对话框View路径
        /// </summary>
        public string DIALOGVIEWPATH = "";

        /// <summary>
        /// 普通View路径
        /// </summary>
        public string NORMALVIEWPATH = "";

        /// <summary>
        /// 前台会员View
        /// </summary>
        private string MEMBERVIEWPATH = "";

        /// <summary>
        /// 控件View路径
        /// </summary>
        public string CONTROLPATH = "";

        /// <summary>
        /// 主题路径
        /// </summary>
        public string CURRENTABSOLUTTHEMPATH = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewPathHelper()
        {
            CACHEOBJECT = CachedConfigContext.Current.FrameworkConfig;
            CURRENTABSOLUTTHEMPATH = CACHEOBJECT.ThemesPath + "/" + CACHEOBJECT.CurrentTheme + "/";

            MANAGEVIEWPATH = CURRENTABSOLUTTHEMPATH + CACHEOBJECT.ManageViewPath;
            DIALOGVIEWPATH = CURRENTABSOLUTTHEMPATH + CACHEOBJECT.DialogViewPath;
            NORMALVIEWPATH = CURRENTABSOLUTTHEMPATH + CACHEOBJECT.NormalViewPath;
            MEMBERVIEWPATH = CURRENTABSOLUTTHEMPATH + CACHEOBJECT.MemberViewPath;
            CONTROLPATH = CURRENTABSOLUTTHEMPATH + CACHEOBJECT.ControlPath + "/";
        }

        #endregion

        /// <summary>
        /// 当前View路径配置信息类实例
        /// </summary>
        /// <returns></returns>
        public static ViewPathHelper Instance()
        {
            return new ViewPathHelper();
        }
    }
}
