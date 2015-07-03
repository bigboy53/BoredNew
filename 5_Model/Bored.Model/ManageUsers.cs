
using System;
using System.ComponentModel.DataAnnotations.Schema;
using DKD.Framework.Contract;

namespace Bored.Model
{
    [Table("ManageUsers")]
    public class ManageUsers : BaseModel
    {
        public ManageUsers()
        {
            this.LastLoginTime = DateTime.Now;
        }
        public string UName { get; set; }
        public string Password { get; set; }
        public string AuthCode { get; set; }
        public int RID { get; set; }
        public string Email { get; set; }
        public string RelName { get; set; }
        public string Tel { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
