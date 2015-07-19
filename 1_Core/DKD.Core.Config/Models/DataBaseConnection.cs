using System;

namespace DKD.Core.Config.Models
{
    [Serializable]
    public class DataBaseConnection : ConfigFileBase
    {
        public DataBaseConnection() { }

        public String Bored { get; set; }
        public String Log { get; set; }
    }
}
