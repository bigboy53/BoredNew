namespace  Manage.ViewModel
{
    public class CollectDto
    {
        #region
        public int ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 类型ID
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// URL地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 数据ID
        /// </summary>
        public int DataID { get; set; }
        /// <summary>
        /// 来源用户
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 来源用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public System.DateTime Publish { get; set; }
        /// <summary>
        /// 收藏时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        #endregion
    }
}
