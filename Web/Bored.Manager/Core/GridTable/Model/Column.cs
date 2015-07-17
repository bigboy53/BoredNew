using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bored.Manager.Core
{
    /// <summary>
    /// 列
    /// </summary>
    public class Column
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string Formatter { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 截取长度
        /// </summary>
        public int SubText { get; set; }
    }
}