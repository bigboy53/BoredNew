using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("Article")]
    public class Article : BaseModel
    {
        public Article()
        {
            this.ArticleImages = new HashSet<ArticleImages>();
        }
        [Key]
        [ForeignKey("ArticleImages")]
        public override int ID { get; set; }
        public string Title { get; set; }
        public int LookCount { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }
        public string Tags { get; set; }
        public ICollection<ArticleImages> ArticleImages { get; set; }
    }
}
