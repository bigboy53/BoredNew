using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("MemberUser")]
    public  class MemberUser : BaseModel
    {
        public string UName { get; set; }
        public string Password { get; set; }
        public string QQ { get; set; }
        public int Sex { get; set; }
        public int Sexual { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}
