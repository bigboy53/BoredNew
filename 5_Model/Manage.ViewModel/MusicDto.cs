using DKD.Framework.Contract.Enum;
using DKD.Framework.Extensions;

namespace  Manage.ViewModel
{
    public class MusicDto
    {
        #region ԭ������
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// �û�ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Song { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Songer { get; set; }
        /// <summary>
        /// ͼƬ
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// ��ַ
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        public string Lyrics { get; set; }
        /// <summary>
        /// ��Դ
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// ��ǩ
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public int ClickCount { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        public string CreateTimeTxt
        {
            get { return CreateTime.ToString("yyyy-MM-dd  HH:mm:ss"); }
        }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// �Ƿ�ΪMV
        /// </summary>
        public bool IsMv { get; set; }

        public string IsMvTxt
        {
            get { return IsMv ? "��" : "��"; }
        }
        #endregion

        #region ��չ����
        public string UserName { get; set; }
        #endregion
    }
}
