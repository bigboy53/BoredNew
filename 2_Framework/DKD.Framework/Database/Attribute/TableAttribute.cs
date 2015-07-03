namespace DKD.Framework.Database.Attribute
{
    public class TableAttribute : System.Attribute
    {
        /// <summary>
        /// 表名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Info { get; set; }

        public TableAttribute(string name)
        {
            Name = name;
            Info = string.Empty;
        }
    }
}
