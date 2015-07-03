using System;
using System.Collections.Generic;
using DKD.Core.Cache;
using DKD.Core.Config.Model;

namespace DKD.Core.Config.Helper
{
    public class Helper
    {
        /// <summary>
        /// 系统配置数据缓存key
        /// </summary>
        private const string CacheKey = "sysconfigmanager";

        /// <summary>
        /// 通过节点获取指定数据
        /// </summary>
        /// <param name="nodeName">系统配置节点名称</param>
        /// <returns></returns>
        public static SysConfigModel GetValue(string nodeName)
        {
            return GetNodeValueByCache(nodeName);
        }

        /// <summary>
        /// 从缓存从获取系统配置数据
        /// </summary>
        /// <param name="nodeName">系统配置节点名称</param>
        /// <returns></returns>
        static SysConfigModel GetNodeValueByCache(string nodeName)
        {//
            Dictionary<string, SysConfigModel> dic = GetSysConfigInfo();
            return dic[nodeName];
        }

        /// <summary>
        /// 从缓存获取配置文件信息。如果不存在则加载至缓存并返回
        /// </summary>
        /// <returns>Dictionary</returns>
        internal static Dictionary<string, SysConfigModel> GetSysConfigInfo()
        {
            var cacheDic = CacheManager.Cache.Get(CacheKey);
            if (cacheDic != null)
                return (Dictionary<string, SysConfigModel>)cacheDic;
            var dic = GetSysConfig();
            CacheManager.Cache.Set(CacheKey,dic);
            return dic;
        }

        /// <summary>
        /// 获取系统配置信息加载至Dictionary
        /// </summary>
        /// <returns>Dictionary</returns>
        static Dictionary<string, SysConfigModel> GetSysConfig()
        {
            Dictionary<string, SysConfigModel> dictmp = XmlUtil.GetSysConfig(SitePathHelper.SysConfigPath, "/sys/config");
            return dictmp;
        }

        /// <summary>
        /// 修改系统配置数据从缓存中
        /// </summary>
        /// <param name="nodeName">系统配置节点名称</param>
        /// <param name="nodeValueModel"></param>
        /// <returns></returns>
        public static bool Update(string nodeName, SysConfigModel nodeValueModel)
        {
            try
            {
                if (UpdateNodeValue(nodeName, nodeValueModel))
                {
                    return  SysConfigManager.CacheDelete();
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 刷新系统配置信息并返回最新的系统配置信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, SysConfigModel> Refresh() 
        {
            bool temp = SysConfigManager.CacheDelete();
            Dictionary<string, SysConfigModel> dic = GetSysConfigInfo();
            if(temp && dic != null)
            {
                return dic;
            }
            return null;
        }


        /// <summary>
        /// 修改单个节点数据
        /// </summary>
        /// <returns></returns>
        static bool UpdateNodeValue(string nodeName, SysConfigModel nodeModel)
        {
            return XmlUtil.UpdateNode(SysConfigManager.SysConfigPath.Value, nodeName, nodeModel);
        }
    }
}
