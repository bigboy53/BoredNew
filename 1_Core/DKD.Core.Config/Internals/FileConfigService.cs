using System;
using System.IO;

namespace DKD.Core.Config
{
    /// <summary>
    /// 配置以文件的形式存在使用目录的Config下
    /// 可以变成DBConfigService即存在数据库中
    /// </summary>
    public class FileConfigService:IConfigService
    {
        private readonly static string ConfigFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        public string GetConfig(string fileName)
        {
            if (!Directory.Exists(ConfigFolder))
                Directory.CreateDirectory(ConfigFolder);
            var configPath = GetFilePath(fileName);
            if (!File.Exists(configPath))
                return null;
            return File.ReadAllText(configPath);
        }

        public void SaveConfig(string fileName, string content)
        {
            var configPath = GetFilePath(fileName);
            File.WriteAllText(configPath, content);
        }

        public string GetFilePath(string fileName)
        {
            var configPath = string.Format(@"{0}\{1}.xml", ConfigFolder, fileName);
            return configPath;
        }
    }
}
