using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("RolePermission")]
    public  class RolePermission : BaseModel
    {
        public int RID { get; set; }
        public string RPName { get; set; }
        public string RPUrl { get; set; }
    }
}
