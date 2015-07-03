using System;
using System.ComponentModel.DataAnnotations;

namespace DKD.Framework.Contract
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreateTime = DateTime.Now;
            this.IsDel = false;
        }
        [Key]
        public virtual int ID { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsDel { get; set; }
    }
}
