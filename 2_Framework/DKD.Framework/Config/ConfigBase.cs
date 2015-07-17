using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DKD.Framework.Config
{
    /// <summary>
    /// 配置文件基类 子类必需重写InitConfig方法,并指定“配置文件的路径”
    /// </summary> 
    public abstract class ConfigBase
    {
        protected ConfigBase()
        { InitConfig(); }

        /// <summary>
        /// 初始化配置文件的路径
        /// </summary>
        public abstract void InitConfig();

        #region 局部变量

        public static readonly string RootPath = HttpContext.Current == null
            ? (AppDomain.CurrentDomain.BaseDirectory) + @"Config\"
            : HttpContext.Current.Server.MapPath("~/Config/");

        /// <summary>
        /// 配置文件所在路径(相对，会应用程序的设目录上加上指定的路径)
        /// </summary>
        protected string SaveJsonPath = "Framework.config";

        /// <summary>
        /// 缓存变量
        /// </summary>
        private static Dictionary<string, ConfigBase> _jsonCache = new Dictionary<string, ConfigBase>();

        #endregion

        #region 实例方法

        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            _jsonCache.Clear();
        }

        /// <summary>
        /// 保存到配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Save<T>()
        {
            File.WriteAllText(RootPath + SaveJsonPath, JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented), System.Text.Encoding.UTF8);

            //加入缓存
            if (!_jsonCache.ContainsKey(this.GetType().GUID.ToString()))
                _jsonCache.Add(this.GetType().GUID.ToString(), this);
            else
                _jsonCache[this.GetType().GUID.ToString()] = this;


        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T RevertJson<T>() where T : ConfigBase, new()
        {
            try
            {

                T temp = JsonConvert.DeserializeObject<T>(File.ReadAllText(RootPath + SaveJsonPath, System.Text.Encoding.UTF8));
                //T temp = JsonConvert.DeserializeObject<T>(File.ReadAllText(@"E:\Bored\Bored\Bored.UnitTest\Config\" + SaveJsonPath, System.Text.Encoding.UTF8));
                //加入缓存
                if (!_jsonCache.ContainsKey(typeof(T).GUID.ToString()))
                    _jsonCache.Add(typeof(T).GUID.ToString(), temp);

                return temp;
            }
            catch
            {
                return default(T);
            }

        }

        #endregion

        #region 实例函数

        /// <summary>
        /// 静态方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Instance<T>() where T : ConfigBase, new()
        {
            //加入缓存
            if (_jsonCache.ContainsKey(typeof(T).GUID.ToString()))
                return (T)_jsonCache[typeof(T).GUID.ToString()];

            return new T().RevertJson<T>();
        }

        /// <summary>
        /// 移除指定类型的缓存
        /// </summary>
        /// <typeparam name="T">要移除对象的类型</typeparam>
        /// <returns></returns>
        public static bool RemoveInstance<T>() where T : ConfigBase, new()
        {
            string typeGuid = typeof(T).GUID.ToString();
            if (_jsonCache.ContainsKey(typeGuid))
            {
                return _jsonCache.Remove(typeGuid);
            }
            return false;
        }


        #endregion

        #region 附助方法

        /// <summary>
        /// 清除Json缓存
        /// </summary>
        public static void ClearJsonCache()
        {
            _jsonCache = new Dictionary<string, ConfigBase>();
        }

        #endregion

        #region 配置缓存依赖处理

        /// <summary>
        /// 初始化配置缓存依赖
        /// </summary>
        public static void StartConfigWatcher()
        {
            var filewatcher = new FileSystemWatcher();
            filewatcher.Filter = "*.*"; //设定监听的文件类型
            filewatcher.Path = RootPath; //设定监听的目录
            filewatcher.Changed += new FileSystemEventHandler(filewatcher_Changed);
            filewatcher.IncludeSubdirectories = true;//设置监听子目录
            filewatcher.EnableRaisingEvents = true;//开始进行监听（其实此处是标示是否进行事件监听和抛出）
        }

        static void filewatcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                //清除配置缓存
                ClearJsonCache();

                //清除数据库链接字符串
                //Database.DbHelper.DbHelperSQL.ConnectionString = ConfigBase.Instance<FrameworkConfig>().ConnectionString.Decrypt();
            }
            catch { }
        }

        #endregion

    }

}