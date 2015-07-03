using System;
using System.Collections.Generic;

namespace  Manage.ViewModel
{
    public class RolesDto
    {
        #region 原有属性
        public int ID { get; set; }
        public string RoleName { get; set; }
        public bool RoleLock { get; set; }

        public string RoleLockTxt
        {
            get { return RoleLock ? "开启" : "关闭"; }
        }
        public virtual DateTime CreateTime { get; set; }
        public virtual bool IsDel { get; set; }

        public string CreateDateTxt
        {
            get { return CreateTime.ToString("yyyy-MM-dd"); }
        }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 权限
        /// </summary>
        public List<RolePermissionDto> RolePermission { get; set; }
        #endregion
    }
}
