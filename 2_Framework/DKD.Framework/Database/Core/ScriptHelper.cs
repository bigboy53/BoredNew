//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace DKD.Framework.Database.Core
//{
//    public class ScriptHelper
//    {
//        /// <summary>
//        /// 获取实体的SQL语句 ，实体源是配置文件里指定的DLL
//        /// </summary>
//        /// <returns></returns>
//        public static string GetTableScript()
//        {
//            StringBuilder sql = new StringBuilder();

//            //从Dll里反射出所有类
//            List<Database.Attribute.TableInfo> ListTableInfo = new List<Attribute.TableInfo>();
//            foreach (Type t in System.Reflection.Assembly.LoadFile((AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory) + @"\" + DKD.Framework.Config.FrameworkConfig.Instance<DKD.Framework.Config.FrameworkConfig>().EntityDll).GetTypes())
//            {
//                ListTableInfo.Add(Database.Attribute.AttributeHelper.GetInfo(t));
//            }

//            //开始生成脚本
//            foreach (Database.Attribute.TableInfo ti in ListTableInfo)
//            {
//                sql.AppendLine(string.Format("create table [{0}]", ti.Table.Name));
//                sql.AppendLine("(");
//                foreach (Database.Attribute.ColumnAttribute ci in ti.Columns)
//                {
//                    object[] values = new object[] {
//                        ci.Name,//字段名
//                        ci.Type.ToString(),//字段类型                        
//                        ci.Length=="-1"?"":string.Format("({0})",ci.Length),
//                        ci.AutoIncrement?string.Format("identity({0})",ci.AutoIncrementString):"",//是否为自动增量列
//                        ci.CanNull?"":"not null",//能否为空
//                        ci.Default==null?"":string.Format("default({0})",ci.Default),//默认值
//                        ci.PrimaryKey?"primary key":""//是否为主键
//                    };

//                    sql.AppendLine(string.Format("	[{0}] {1}{2} {3} {4} {5} {6}", values).TrimEnd(new char[] { ' ' }) + ",");
//                }
//                //移除最后一个","
//                sql.Remove(sql.ToString().LastIndexOf(','), 1);

//                sql.AppendLine(")");
//                sql.AppendLine("go");
//            }

//            return sql.ToString();
//        }

//        //获取实体的SQL语句并保存到文件
//        public static void SqlToFile(string filePath)
//        {
//            System.IO.File.WriteAllText(filePath, GetTableScript(), System.Text.Encoding.UTF8);
//        }

//        /// <summary>
//        /// 获取数据访问层的DLL
//        /// </summary>
//        /// <returns></returns>
//        public static string GenerateAccessObject()
//        {
//            StringBuilder classCode = new StringBuilder();
//            foreach (Type t in System.Reflection.Assembly.LoadFile((AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory) + @"\" + DKD.Framework.Config.FrameworkConfig.Instance<DKD.Framework.Config.FrameworkConfig>().EntityDll).GetTypes())
//            {
//                object[] temp = t.GetCustomAttributes(typeof(DKD.Framework.Database.Attribute.TableAttribute), false);

//                if (temp != null && temp.Length > 0 && temp[0].GetType().FullName.ToUpper().Trim() == "DKD.FRAMEWORK.DATABASE.ATTRIBUTE.TABLEATTRIBUTE")
//                    classCode.AppendLine(string.Format("DKD.Framework.EntityCore<{0}>.DeleteWhere(\"1=2\");", t.FullName));
//            }

//            return classCode.ToString();
//        }
//    }
//}
