
namespace DKD.Framework.Database.Attribute
{
    public class TableInfo
    {
        //类对应的表的相关信息
        public TableAttribute Table { get; set; }

        //类对应的列的相关信息
        public ColumnAttribute[] Columns { get; set; }

        /// <summary>
        /// 获取模型的命名空间全称
        /// </summary>
        public string TypeFullName { get; set; }

    }
}
