using DKD.Framework.Contract.Enum;
using DKD.Framework.Extensions;

namespace  Manage.ViewModel
{
    public class GameDto
    {
        #region ԭ������
        public int ID { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ͼƬ
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// ��ַ
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public int ClickCount { get; set; }
        /// <summary>
        /// ��Դ
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// ��ǩ
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        public string CreateTimeTxt
        {
            get { return CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDel { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ������
        /// </summary>
        public string UserName { get; set; }
        #endregion
    }
}
