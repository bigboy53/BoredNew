using System.Collections.Generic;

namespace Manage.ViewModel
{
    public class ArticleDto
    {
        #region ԭ������
        public int ID { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ͼƬ
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public int LookCount { get; set; }
        /// <summary>
        /// ��Դ
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// ��Դ�ı�
        /// </summary>
        public string SourceTxt { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ��ǩ
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public System.DateTime CreatTime { get; set; }

        public string CreateTimeTxt
        {
            get { return CreatTime.ToString("yyyy-MM-dd"); }
        }

        #endregion

        #region ��չ����
        /// <summary>
        /// ������
        /// </summary>
        public string UserName { get; set; }
        public List<ArticleImagesDto> ArticleImages { get; set; }
        #endregion
    }
}
