using System;
using DKD.Framework.Utility.Xml;

namespace DKD.Core.Config
{
    public class ConfigContext
    {
        public readonly IConfigService ConfigService;

        #region 构造函数
        public ConfigContext()
            : this(new FileConfigService())
        {

        }

        public ConfigContext(IConfigService auditable)
        {
            ConfigService = auditable;
        }
        #endregion

        public virtual T Get<T>(string index = null) where T : ConfigFileBase, new()
        {
            //VerificationIndex(result, index);
            var result = this.GetConfigFile<T>(index);
            return result;
        }

        public void Save<T>(T configFile, string index = null) where T : ConfigFileBase
        {
            configFile.Save();
            var fileName = this.GetConfigFileName<T>(index);
            this.ConfigService.SaveConfig(fileName, SerializationHelper.XmlSerialize(configFile));
        }

        private T GetConfigFile<T>(string index = null) where T : ConfigFileBase, new()
        {
            var result = default(T);
            var fileName = this.GetConfigFileName<T>(index);
            var content = this.ConfigService.GetConfig(fileName);
            if(content==null)
                this.ConfigService.SaveConfig(fileName,string.Empty);
            else if (!string.IsNullOrEmpty(content))
            {
                try
                {
                    result = (T)SerializationHelper.XmlDeserialize(typeof(T), content);
                }
                catch
                {
                    result = new T();
                }
            }
            return result;
        }

        public virtual string GetConfigFileName<T>(string index = null)
        {
            var fileName = typeof (T).Name;
            if (!string.IsNullOrEmpty(index))
                fileName = string.Format("{0}_{1}", fileName, index);
            return fileName;
        }

        /// <summary>
        /// 判断文件分区索引（暂时不用）
        /// </summary>
        [Obsolete]
        public virtual void VerificationIndex<T>(T configFile, string index) where T : ConfigFileBase
        {
            if (configFile.IsHasIndex && string.IsNullOrEmpty(index))
                throw new Exception("调用时没有提供配置文件的分区索引");
        }
    }
}
