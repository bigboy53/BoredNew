
using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("ConfigInfo")]
    public class ConfigInfo : BaseModel
    {
        public string Name { get; set; }
        public int Type { get; set; }
    }
}
