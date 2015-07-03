using DKD.Framework.Contract.Enum;
using DKD.Framework.Extensions;

namespace  Manage.ViewModel
{
    public class GameDto
    {
        #region 原有属性
        public int ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        public string CreateTimeTxt
        {
            get { return CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 创建人
        /// </summary>
        public string UserName { get; set; }
        #endregion
    }
}
