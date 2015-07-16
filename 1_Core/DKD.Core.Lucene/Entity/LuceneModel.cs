using System;

namespace DKD.Core.Lucene
{
    public class LuceneModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 类型(文章、游戏、视频等)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// 
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// lucene.net无法对时间字段进行排序和区域检索，所以，要把时间字段转成长整形来实现
        /// var time = DateTime.Now;
        /// var timeField = new NumericField("Publish", Field.Store.YES, true).SetLongValue(time.Ticks);
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 图片(多图用,分割)
        /// </summary>
        public string Images { get; set; }
        public string Tags { get; set; }
        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public LuceneType IndexType { get; set; }
        /// <summary>
        /// 高亮显示标题
        /// </summary>
        public string HightLightTitle { get; set; }
        /// <summary>
        /// 高亮显示内容
        /// </summary>
        public string HightLightContent { get; set; }
    }
}
