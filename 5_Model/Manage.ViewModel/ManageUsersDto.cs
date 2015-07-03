using System;

namespace  Manage.ViewModel
{
    public class ManageUsersDto
    {
        #region 原有属性
        public int ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 授权码
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public int RID { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 真实名字
        /// </summary>
        public string RelName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 上传登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        #endregion
    }
}
