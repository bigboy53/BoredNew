using DKD.Framework.Contract.Enum;
using DKD.Framework.Extensions;

namespace  Manage.ViewModel
{
    public class MusicDto
    {
        #region 原有属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 歌名
        /// </summary>
        public string Song { get; set; }
        /// <summary>
        /// 歌手
        /// </summary>
        public string Songer { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 歌词
        /// </summary>
        public string Lyrics { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        public string CreateTimeTxt
        {
            get { return CreateTime.ToString("yyyy-MM-dd  HH:mm:ss"); }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 是否为MV
        /// </summary>
        public bool IsMv { get; set; }

        public string IsMvTxt
        {
            get { return IsMv ? "是" : "否"; }
        }
        #endregion

        #region 扩展属性
        public string UserName { get; set; }
        #endregion
    }
}
