using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using BoredTool.Properties;

namespace BoredTool
{
    public partial class From1 : Form
    {
        public From1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateClass_Click(object sender, EventArgs e)
        {
            try
            {
                var str = new StringBuilder();
                str.AppendFormat(@"{0}

                namespace {1}
                {4}                        
                    public class {3}Context : DbContext
                    {4}
                        public {3}Context()
                                : base({2})
                        {4}
                        {5}
                     public DbSet<{3}> {3} {4} get; set; {5}
                    {5}

                    /// <summary>
                    /// {6}
                    /// </summary>
                    public class {3}
                    {4}
                    ", used.Text, NameSpace.Text, Connection.Text.GeQuotationStr(), ClassName.Text, "{", "}",TableName.Text);

                object colum;
                object dbType;
                if (list.RowCount < 2)
                {
                    MessageBox.Show("不能为空！");
                    return;
                }
                int index = 1;
                foreach (DataGridViewRow item in list.Rows)
                {
                    if (index == list.RowCount)
                        break;
                    index++;
                    colum = item.Cells["Colum"].Value;
                    if (colum == null)
                    {
                        MessageBox.Show("列名不能为空！");
                        return;
                    }
                    dbType = item.Cells["DataType"].Value;
                    if (dbType == null)
                    {
                        MessageBox.Show("数据类型不能为空！");
                        return;
                    }
                    str.AppendFormat(@"
                    /// <summary>
                    /// {4}
                    /// </summary>
                    public {2} {3} {0} get; set; {1}
                    ", "{","}", dbType.DbTypeToDotNetDataType(item.Cells["IsNull"].Value != null), colum, item.Cells["Description"].Value);
                }
                str.Append(@"}
                                        }");

                Clipboard.SetData(DataFormats.Text,str.ToString());
                MessageBox.Show("已复制到剪切板！");
            }
            catch
            {
                MessageBox.Show("报错了，擦！");
            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (list.SelectedRows.Count>0) { 
                foreach (DataGridViewRow item in list.SelectedRows)
                {
                    list.Rows.RemoveAt(item.Index);
                }
                }
            }
            catch
            {
            }
        }

        private void CreateSQL_Click(object sender, EventArgs e)
        {
            try
            {
                string fname = "";
                var openfiledialog = new OpenFileDialog {Multiselect = false, Filter = Resources.Filter};
                if (openfiledialog.ShowDialog() == DialogResult.OK)
                    fname = openfiledialog.FileName;
                var f1 = new SQL(GetTableScript(fname));
                f1.ShowDialog();
            }
            catch
            {
            }
        }

        private static string GetTableScript(string path)
        {
            var sql = new StringBuilder();

            //从Dll里反射出所有类
            var listTableInfo = new List<DKD.Framework.Database.Attribute.TableInfo>();
            foreach (var t in System.Reflection.Assembly.LoadFile(path).GetTypes())
            {
                //listTableInfo.Add(DKD.Framework.Database.Attribute.AttributeHelper.GetInfo(t));
            }

            //开始生成脚本
            foreach (var ti in listTableInfo)
            {
                sql.AppendLine(string.Format("create table [{0}]", ti.Table.Name));
                sql.AppendLine("(");
                foreach (var ci in ti.Columns)
                {
                    var values = new object[] {
                        ci.Name,//字段名
                        ci.Type.ToString(),//字段类型                        
                        ci.Length=="-1"?"":string.Format("({0})",ci.Length),
                        ci.AutoIncrement?string.Format("identity({0})",ci.AutoIncrementString):"",//是否为自动增量列
                        ci.CanNull?"":"not null",//能否为空
                        ci.Default==null?"":string.Format("default({0})",ci.Default),//默认值
                        ci.PrimaryKey?"primary key":""//是否为主键
                    };

                    sql.AppendLine(string.Format("	[{0}] {1}{2} {3} {4} {5} {6}", values).TrimEnd(new[] { ' ' }) + ",");
                }
                //移除最后一个","
                sql.Remove(sql.ToString().LastIndexOf(','), 1);

                sql.AppendLine(")");
                sql.AppendLine("go");
            }

            return sql.ToString();
        }

        private void Encryption_Click(object sender, EventArgs e)
        {
            var f=new Encryption();
            f.ShowDialog();
        }

        private void list_TabIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("sadf");
            //ImeMode = ImeMode.Off;
        }

        private void list_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
        }

        private void list_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("list_CellEnter");
            ImeMode = ImeMode.Off;
            
        }

        private void list_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void list_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("list_RowLeave");
        }
    }

    public static class Exptent
    {
        public static string GeQuotationStr(this string str)
        {
            return "\""+str+"\"";
        }

        public static string GetEmptyStr(this object str,string defaultStr)
        {
            if (str!=null&&!string.IsNullOrEmpty(str.ToString()))
                return str.ToString();
            return defaultStr;
        }
        // SqlDbType转换为C#数据类型
        public static string DbTypeToDotNetDataType(this object dbType,bool isNull)
        {
            string str;
            switch (dbType.ToString().ToLower())
            {
                case "bigint": str="int"; break;
                case "binary": str="Byte[]"; break;
                case "bit": str="Boolean"; break;
                case "char": str="String"; break;
                case "datetime": str="DateTime"; break;
                case "decimal": str="Decimal"; break;
                case "float": str="Double"; break;
                case "image": str="Byte[]"; break;
                case "int": str="int"; break;
                case "money": str=" Decimal"; break;
                case "nchar": str="String"; break;
                case "ntext": str="String"; break;
                case "numeric": str="Decimal"; break;
                case "nvarchar": str="String"; break;
                case "real": str="Single"; break;
                case "uniqueidentifier": str="Guid"; break;
                case "smalldatetime": str="DateTime"; break;
                case "smallint": str="int"; break;
                case "smallmoney": str="Decimal"; break;
                case "text": str="String"; break;
                case "timestamp": str="Byte[]"; break;
                case "tinyint": str="Byte"; break;
                case "varbinary": str="Byte[]"; break;
                case "varchar": str="String"; break;
                case "variant": str="Object"; break;
                default:
                    return "";
            }
            if (str != "" && isNull)
                return str + "?";
            return str;
        }
    }

    
}
#region 以前的ORM
//                //var str = new StringBuilder();
//                str.AppendFormat(@"{0}
//                
//                
//                                                namespace {1}
//                                                {{
//                                                /// <summary>
//                                                /// {4}
//                                                /// </summary>
//                                                [Table({2}, Info = {4})]
//                                                 public class {3}
//                                                {{", used.Text, NameSpace.Text, ClassName.Text.GeQuotationStr(), ClassName.Text, TableName.Text.GeQuotationStr());
//                //object colum;
//                //object dbType;
//                if (list.RowCount < 2)
//                {
//                    MessageBox.Show("不能为空！");
//                    return;
//                }
//                //int index = 1;
//                foreach (DataGridViewRow item in list.Rows)
//                {
//                    if (index == list.RowCount)
//                        break;
//                    index++;
//                    str.AppendFormat(@"
//                                    
//                                    /// <summary>
//                                    /// {0}
//                                    /// </summary>
//                                    [",
//                        item.Cells["Description"].Value);
//                    colum = item.Cells["Colum"].Value;
//                    if (colum == null)
//                    {
//                        MessageBox.Show("列名不能为空！");
//                        return;
//                    }
//                    dbType = item.Cells["DataType"].Value;
//                    if (dbType == null)
//                    {
//                        MessageBox.Show("数据类型不能为空！");
//                        return;
//                    }
//                    str.AppendFormat("Column(\"{0}\",", colum);
//                    str.AppendFormat("System.Data.SqlDbType.{0}", dbType);
//                    if (item.Cells["ZhuJian"].Value != null)
//                        str.Append(",PrimaryKey=true");
//                    str.AppendFormat(",Length=\"{0}\"", item.Cells["Length"].Value.GetEmptyStr("-1"));
//                    if (item.Cells["AutoIncrement"].Value != null)
//                        str.AppendFormat(",AutoIncrement={0},AutoIncrementString=\"{1}\"", "true",
//                            item.Cells["AutoIncrement"].Value);
//                    if (item.Cells["Default"].Value != null)
//                        str.AppendFormat(",Default={0}", item.Cells["Default"].Value);
//                    str.AppendFormat(@")]
//                                    public {0} {1} {{ get; set; }}", dbType.DbTypeToDotNetDataType(item.Cells["IsNull"].Value != null),
//                        colum);
//                }
//                str.Append(@"}
//                                        }");
                #endregion