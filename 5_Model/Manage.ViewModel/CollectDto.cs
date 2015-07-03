namespace  Manage.ViewModel
{
    public class CollectDto
    {
        #region
        public int ID { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// URL��ַ
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>
        public int DataID { get; set; }
        /// <summary>
        /// ��Դ�û�
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// ��Դ�û���
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// ����ͼ
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public System.DateTime Publish { get; set; }
        /// <summary>
        /// �ղ�ʱ��
        /// </summary>
        public System.DateTime CreateTime { get; set; }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDel { get; set; }
        #endregion
    }
}
