using System;

namespace  Manage.ViewModel
{
    public class ManageUsersDto
    {
        #region ԭ������
        public int ID { get; set; }
        /// <summary>
        /// �û���
        /// </summary>
        public string UName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// ��Ȩ��
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// ��ɫid
        /// </summary>
        public int RID { get; set; }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// ע��ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ��ʵ����
        /// </summary>
        public string RelName { get; set; }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// �ϴ���¼ʱ��
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public string RoleName { get; set; }
        #endregion
    }
}
