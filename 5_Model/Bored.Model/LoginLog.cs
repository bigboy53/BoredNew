using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("LoginLog")]
    public class LoginLog : BaseModel
    {
        public string UName { get; set; }
        public string IP { get; set; }
    }
}
