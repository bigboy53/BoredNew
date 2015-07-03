using System.Collections.Generic;
using System.Linq;
using DKD.Core.Config.Model;


namespace DKD.Core.Config
{
    /// <summary>
    /// 系统配置信息管理类
    /// 
    /// </summary>
    public class SysConfigManager
    {
        #region 操作XML
        /// <summary>
        /// 系统配置hashmap数据格式
        /// </summary>
        public static Dictionary<string, SysConfigModel> DicSysConfig
        {
            get
            {
                return Helper.Helper.GetSysConfigInfo();
            }
        }

        /// <summary>
        /// 根据传入的参数进行分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public static Dictionary<string, SysConfigModel> GetByPage(int pageIndex, int pageSize, out int dataCount)
        {
            dataCount = DicSysConfig.Count;
            var result = DicSysConfig.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return result.ToDictionary(k => k.Key, v => v.Value);
        }

        /// <summary>
        /// 修改单个配置对象
        /// </summary>
        /// <param name="key">需要修改的键</param>
        /// <param name="model">需要修改的值</param>
        /// <returns></returns>
        public bool Update(string key, SysConfigModel model)
        {
            return Helper.Helper.Update(key, model);
        }

        /// <summary>
        /// 获取单个配置对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public SysConfigModel GetModel(string key)
        {
            return Helper.Helper.GetValue(key);
        }

        /// <summary>
        /// 将系统配置数据从缓存删除
        /// </summary>
        /// <returns></returns>
        public static bool CacheDelete()
        {
            //object obj = Plugin.Memcached.LocalCache.Delete(SysConfig.Helper.Helper.CacheKey, Plugin.CacheModel.CacheSeverType.DATA);
            //return obj != null ? true : false;
            return true;
        }
        #endregion

        #region 配置对象

        /// <summary>
        /// 地区cookie过期时间
        /// </summary>
        public static SysConfigModel ArddessCookieExpirationTime
        {
            get
            {
                return Helper.Helper.GetValue("ArddessCookieExpirationTime");
            }
        }
        public static SysConfigModel SysConfigPath
        {
            get
            {
                return Helper.Helper.GetValue("SysConfigPath");
            }
        }
        public static SysConfigModel UnRarExePath
        {
            get
            {
                return Helper.Helper.GetValue("UnRarExePath");
            }
        }
        #endregion

    }
}
