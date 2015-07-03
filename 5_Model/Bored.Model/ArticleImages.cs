using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("ArticleImages")]
    public class ArticleImages : BaseModel
    {
        public int AID { get; set; }
        public string URL { get; set; }
        public int Number { get; set; }
        [ForeignKey("AID")]
        public virtual Article Article { get; set; }
    }
}
