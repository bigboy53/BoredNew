using System;

namespace DKD.Core.Config.Models
{
    [Serializable]
    public class FrameworkConfig : ConfigFileBase
    {
        public FrameworkConfig()
        {
        }
        #region 框架属性

        /// <summary>
        /// 是否用Session来保存用户信息,True：用Session保存 False：用Cookies来保存
        /// </summary> 
        public bool IsSessionAuthor { get; set; }

        /// <summary>
        /// Cookies过期时间
        /// </summary>
        public int CookiesTimer { get; set; }

        /// <summary>
        /// 后台管理员登陆信息Key
        /// </summary>
        public string ManageAuthorKey { get; set; }

        /// <summary>
        /// 前台会员登陆信息Key
        /// </summary>
        public string MemberAuthorKey { get; set; }

        /// <summary>
        /// IP数据库所在地址
        /// </summary>
        public string IPDatabasePath { get; set; }
        /// <summary>
        /// Cookies 域
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 动态编译DLL的文件前缀名
        /// </summary>
        public string DllName { get; set; }

        #endregion

        #region 代码属性
        /// <summary>
        /// 要进行权限反射的Dlls，多个Dll以","分隔
        /// </summary>
        
        public string ControllerRefs { get; set; }

        #endregion

        #region 短信属性

        /// <summary>
        /// Smtp服务器地下
        /// </summary>
        
        public string EmailHost { get; set; }

        /// <summary>
        /// 电子邮箱用户名
        /// </summary>
        
        public string EmailUser { get; set; }

        /// <summary>
        /// 电子邮箱密码
        /// </summary>
        
        public string EmailPassword { get; set; }

        /// <summary>
        /// 电子邮箱商品
        /// </summary>
        
        public int EmailPort { get; set; }

        /// <summary>
        /// 管理员电子邮箱
        /// </summary>
        
        public string MasterEmail { get; set; }

        #endregion

        #region MVC视图属性

        /// <summary>
        /// 后台管理View 路径
        /// </summary>
        
        public string ManageViewPath { get; set; }

        /// <summary>
        /// 对话框View 路径
        /// </summary>
        
        public string DialogViewPath { get; set; }

        /// <summary>
        /// 普通View 路径
        /// </summary>
        
        public string NormalViewPath { get; set; }

        /// <summary>
        /// 前台会员View路径
        /// </summary>
        
        public string MemberViewPath { get; set; }

        /// <summary>
        /// 页面主题所在路径
        /// </summary>
        
        public string ThemesPath { get; set; }

        /// <summary>
        /// 当前主题
        /// </summary>
        
        public string CurrentTheme { get; set; }

        /// <summary>
        /// 通用控件所在路径
        /// </summary>
        
        public string ControlPath { get; set; }

        #endregion
    }

}
