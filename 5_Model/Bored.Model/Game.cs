using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("Game")]
    public class Game : BaseModel
    {
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public int ClickCount { get; set; }
        public string Source { get; set; }
        public string Tags { get; set; }
    }
}
