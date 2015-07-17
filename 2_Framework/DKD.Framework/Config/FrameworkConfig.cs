using Newtonsoft.Json;

namespace DKD.Framework.Config
{
    /// <summary>
    /// DKD FrameWork
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class FrameworkConfig : ConfigBase
    {
        #region 框架属性

        /// <summary>
        /// 是否用Session来保存用户信息,True：用Session保存 False：用Cookies来保存
        /// </summary> 
        [JsonProperty]
        public bool IsSessionAuthor { get; set; }

        /// <summary>
        /// Cookies过期时间
        /// </summary>
        [JsonProperty]
        public int CookiesTimer { get; set; }

        /// <summary>
        /// 后台管理员登陆信息Key
        /// </summary>
        [JsonProperty]
        public string ManageAuthorKey { get; set; }

        /// <summary>
        /// 前台会员登陆信息Key
        /// </summary>
        [JsonProperty]
        public string MemberAuthorKey { get; set; }

        /// <summary>
        /// 上传文件的路径
        /// </summary>
        [JsonProperty]
        public string UploadFilePath { get; set; }

        /// <summary>
        /// IP数据库所在地址
        /// </summary>
        [JsonProperty]
        public string IPDatabasePath { get; set; }

        #endregion

        #region 代码属性

        /// <summary>
        /// 动态编译DLL的命名空间
        /// </summary>
        [JsonProperty]
        public string Namespace { get; set; }

        /// <summary>
        /// 动态编译DLL的文件前缀名
        /// </summary>
        [JsonProperty]
        public string DllName { get; set; }

        /// <summary>
        /// 数据库的表前缀名
        /// </summary>
        [JsonProperty]
        public string TablePrefix { get; set; }

        /// <summary>
        /// 数据库的链接字符串
        /// </summary>
        [JsonProperty]
        public string ConnectionString { get; set; }

        /// <summary>
        /// 实体层所在的Dll
        /// </summary>
        [JsonProperty]
        public string EntityDll { get; set; }

        /// <summary>
        /// 要进行权限反射的Dlls，多个Dll以","分隔
        /// </summary>
        [JsonProperty]
        public string ControllerRefs { get; set; }

        /// <summary>
        /// Cookies 域
        /// </summary>
        [JsonProperty]
        public string Domain { get; set; }

        #endregion

        #region 短信属性

        /// <summary>
        /// Smtp服务器地下
        /// </summary>
        [JsonProperty]
        public string EmailHost { get; set; }

        /// <summary>
        /// 电子邮箱用户名
        /// </summary>
        [JsonProperty]
        public string EmailUser { get; set; }

        /// <summary>
        /// 电子邮箱密码
        /// </summary>
        [JsonProperty]
        public string EmailPassword { get; set; }

        /// <summary>
        /// 电子邮箱商品
        /// </summary>
        [JsonProperty]
        public int EmailPort { get; set; }

        /// <summary>
        /// 管理员电子邮箱
        /// </summary>
        [JsonProperty]
        public string MasterEmail { get; set; }

        #endregion

        #region MVC视图属性

        /// <summary>
        /// 后台管理View 路径
        /// </summary>
        [JsonProperty]
        public string ManageViewPath { get; set; }

        /// <summary>
        /// 对话框View 路径
        /// </summary>
        [JsonProperty]
        public string DialogViewPath { get; set; }

        /// <summary>
        /// 普通View 路径
        /// </summary>
        [JsonProperty]
        public string NormalViewPath { get; set; }

        /// <summary>
        /// 前台会员View路径
        /// </summary>
        [JsonProperty]
        public string MemberViewPath { get; set; }

        /// <summary>
        /// 页面主题所在路径
        /// </summary>
        [JsonProperty]
        public string ThemesPath { get; set; }

        /// <summary>
        /// 当前主题
        /// </summary>
        [JsonProperty]
        public string CurrentTheme { get; set; }

        /// <summary>
        /// 通用控件所在路径
        /// </summary>
        [JsonProperty]
        public string ControlPath { get; set; }

        #endregion


        public override void InitConfig()
        {
        }
    }
}
