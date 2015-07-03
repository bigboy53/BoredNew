using System;
using DKD.Framework.Contract.Enum;
using DKD.Framework.Extensions;

namespace  Manage.ViewModel
{
    public class VideoDto
    {
        #region 原有属性
        public int ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public int UserId { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        public string SourceTxt { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public string CreateTimeTxt
        {
            get { return CreateTime.ToString("yyyy-MM-dd HH:ss:mm"); }
        }
        public bool IsDel { get; set; }
        #endregion
    }
}
