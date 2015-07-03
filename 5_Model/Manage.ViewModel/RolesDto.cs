using System;
using System.Collections.Generic;

namespace  Manage.ViewModel
{
    public class RolesDto
    {
        #region ԭ������
        public int ID { get; set; }
        public string RoleName { get; set; }
        public bool RoleLock { get; set; }

        public string RoleLockTxt
        {
            get { return RoleLock ? "����" : "�ر�"; }
        }
        public virtual DateTime CreateTime { get; set; }
        public virtual bool IsDel { get; set; }

        public string CreateDateTxt
        {
            get { return CreateTime.ToString("yyyy-MM-dd"); }
        }
        #endregion

        #region ��չ����
        /// <summary>
        /// Ȩ��
        /// </summary>
        public List<RolePermissionDto> RolePermission { get; set; }
        #endregion
    }
}
