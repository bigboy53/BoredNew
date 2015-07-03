using System.Collections.Generic;

namespace Manage.ViewModel
{
    public class ArticleDto
    {
        #region 原有属性
        public int ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int LookCount { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 来源文本
        /// </summary>
        public string SourceTxt { get; set; }
        /// <summary>
        /// 发表人
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreatTime { get; set; }

        public string CreateTimeTxt
        {
            get { return CreatTime.ToString("yyyy-MM-dd"); }
        }

        #endregion

        #region 扩展属性
        /// <summary>
        /// 发布人
        /// </summary>
        public string UserName { get; set; }
        public List<ArticleImagesDto> ArticleImages { get; set; }
        #endregion
    }
}
