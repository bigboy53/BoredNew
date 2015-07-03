using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("Roles")]
    public class Roles : BaseModel
    {
        public string RoleName { get; set; }
        public bool RoleLock { get; set; }
    }
}
