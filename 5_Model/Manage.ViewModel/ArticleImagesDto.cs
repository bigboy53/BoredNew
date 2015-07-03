using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.ViewModel
{
    public class ArticleImagesDto
    {
        public int ID { get; set; }
        public int AID { get; set; }
        public string URL { get; set; }
        public int Number { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual bool IsDel { get; set; }
    }
}
