
using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("Music")]
    public class Music : BaseModel
    {
        public int UserId { get; set; }
        public bool IsMv { get; set; }
        public string Song { get; set; }
        public string Songer { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Lyrics { get; set; }
        public int ClickCount { get; set; }
        public string Source { get; set; }
        public string Tags { get; set; }
    }
}
