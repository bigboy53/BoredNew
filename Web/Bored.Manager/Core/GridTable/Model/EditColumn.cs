using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bored.Manager.Core
{
    /// <summary>
    /// 修改列
    /// </summary>
    public class EditColumn
    {
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 操作地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string Formatter { get; set; }
    }
}