using System;
using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("Comment")]
    public class Comment : BaseModel
    {
        public string Title { get; set; }
        public int? TypeID { get; set; }
        public string Url { get; set; }
        public int? DataID { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public DateTime? Publish { get; set; }
        public string Content { get; set; }
    }
}
