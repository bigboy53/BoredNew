using System;

namespace  Manage.ViewModel
{

    public class LoginLogDto
    {
        public int ID { get; set; }
        public string UName { get; set; }
        public string IP { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual bool IsDel { get; set; }
    }
}
