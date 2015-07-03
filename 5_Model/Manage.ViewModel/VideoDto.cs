using System;
using DKD.Framework.Contract.Enum;
using DKD.Framework.Extensions;

namespace  Manage.ViewModel
{
    public class VideoDto
    {
        #region ԭ������
        public int ID { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        public int UserId { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public int ClickCount { get; set; }
        /// <summary>
        /// ��ַ
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// ����ͼ
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ��Դ
        /// </summary>
        public string Source { get; set; }
        public string SourceTxt { get; set; }
        /// <summary>
        /// ��ǩ
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// ����ʱ��
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
