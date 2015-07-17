using System;

namespace DKD.Framework.Utility.Files
{
    [Serializable]
    public class FileModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 完整目录
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// 是否是文件夹
        /// </summary>
        public bool IsFolder { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime LastAccessDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastWriteDate { get; set; }

        /// <summary>
        /// 文件数
        /// </summary>
        public long FileCount { get; set; }

        /// <summary>
        /// 文件夹数
        /// </summary>
        public long SubFolderCount { get; set; }

        /// <summary>
        /// 获取版本
        /// </summary>
        /// <returns>返回版本字符串</returns>
        public string Version()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }
    }
}
