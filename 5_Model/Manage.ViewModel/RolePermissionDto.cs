using System;

namespace  Manage.ViewModel
{
    public class RolePermissionDto
    {
        public int ID { get; set; }
        public int RID { get; set; }
        public string RPName { get; set; }
        public string RPUrl { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual bool IsDel { get; set; }
    }
}
