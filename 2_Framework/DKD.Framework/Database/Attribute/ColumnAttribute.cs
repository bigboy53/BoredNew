using System.Data;
namespace DKD.Framework.Database.Attribute
{
    public class ColumnAttribute : System.Attribute
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public SqlDbType Type { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 是否能为空
        /// </summary>
        public bool CanNull { get; set; }

        /// <summary>
        /// 默认值 
        /// </summary>
        public object Default { get; set; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool PrimaryKey { get; set; }

        /// <summary>
        /// 自动增加列
        /// </summary>
        public bool AutoIncrement { get; set; }

        /// <summary>
        /// 默认自动增加列的字符 （"1,1"）
        /// </summary>
        public string AutoIncrementString { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ColumnAttribute(string name, SqlDbType type)
        {
            Name = name;
            Type = type;
            Length = "-1";
            Intro = string.Empty;
            CanNull = true;
            Default = null;
            PrimaryKey = false;
            AutoIncrement = false;
            AutoIncrementString = "1,1";
        }
    }
}
