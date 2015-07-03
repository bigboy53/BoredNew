using System;
using System.Xml;
using System.Collections.Generic;
using System.Data;
using DKD.Core.Config.Model;

namespace DKD.Core.Config.Helper
{
    /// <summary>
    /// XML相关通用功能
    /// </summary>
    public class XmlUtil
    {
        public XmlUtil() { }


        /// <summary>
        /// 读取XML资源到Dictionary中
        /// </summary>
        /// <param name="source">XML资源，文件为路径，否则为XML字符串</param>
        /// <param name="fisertnodes">解析节点名称</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, SysConfigModel> GetSysConfig(string source, string fisertnodes)
        {
            var fileName = source;
            var doc = new XmlDocument();
            doc.Load(fileName);
            var dic = new Dictionary<string, SysConfigModel>();
            //建立表结构
            var songNode = doc.SelectSingleNode(fisertnodes);
            if (songNode != null)
            {
                for (var i = 0; i < songNode.ChildNodes.Count; i++)
                {
                    if (songNode.NodeType != XmlNodeType.Comment)
                    {
                        var colName = songNode.ChildNodes.Item(i).Name;
                        if (colName != "#comment")
                        {
                            var node = songNode.ChildNodes.Item(i);
                            var model = new SysConfigModel();
                            if (node != null)
                            {
                                model.Value = node.InnerText.Trim();
                                if (node.Attributes != null)
                                {
                                    model.Title = node.Attributes["title"].InnerText;
                                    model.Descrption = node.Attributes["descrption"].InnerText;
                                }
                                dic.Add(node.Name,model);
                            }
                        }
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// XML资源类型
        /// </summary>
        public enum XmlType
        {
            File,
            String
        };

        #region 读取XML资源到DataSet中
        /// <summary>
        /// 读取XML资源到DataSet中
        /// </summary>
        /// <param name="source">XML资源，文件为路径，否则为XML字符串</param>
        /// <param name="xmlType">XML资源类型</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string source, XmlType xmlType)
        {
            DataSet ds = new DataSet();
            if (xmlType == XmlType.File)
            {
                ds.ReadXml(source);
            }
            else
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(source);
                XmlNodeReader xnr = new XmlNodeReader(xd);
                ds.ReadXml(xnr);
            }

            return ds;
        }

        #endregion

        #region 获取一个字符串xml文档中的ds

        /// <remarks>
        /// 
        /// </remarks>
        ///  <summary>
        ///  获取一个字符串xml文档中的ds
        ///  </summary>
        ///  <param name="xmlString">含有xml信息的字符串</param>
        /// <param name="ds"></param>
        public static void get_XmlValue_ds(string xmlString, ref DataSet ds)
        {

            var xd = new XmlDocument();
            xd.LoadXml(xmlString);
            XmlNodeReader xnr = new XmlNodeReader(xd);
            ds.ReadXml(xnr);
            xnr.Close();
        }

        #endregion

        #region  读取XML资源到DataTable中
        /// <summary>
        /// 读取XML资源到DataTable中
        /// </summary>
        /// <param name="source">XML资源，文件为路径，否则为XML字符串</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="tableName">表名称</param>
        /// <returns>DataTable</returns>
        public static DataTable GetTable(string source, XmlType xmlType, string tableName)
        {
            DataSet ds = new DataSet();
            if (xmlType == XmlType.File)
            {
                ds.ReadXml(source);
            }
            else
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(source);
                XmlNodeReader xnr = new XmlNodeReader(xd);
                ds.ReadXml(xnr);
            }

            return ds.Tables[tableName];
        }


        /// <summary>
        /// 读取XML资源到DataTable中
        /// </summary>
        /// <param name="source">XML资源，文件为路径，否则为XML字符串</param>
        /// <param name="fisertnodes"></param>
        /// <returns>DataTable</returns>
        public static DataTable GetTable(string source, string fisertnodes)
        {
            if (fisertnodes == null) throw new ArgumentNullException("fisertnodes");
            string fileName = source;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            DataTable dt = new DataTable();
            //建立表结构
            XmlNode songNode = doc.SelectSingleNode(fisertnodes);
            string colName;
            if (songNode != null)
            {
                for (int i = 0; i < songNode.ChildNodes.Count; i++)
                {
                    if (songNode.NodeType != XmlNodeType.Comment)
                    {
                        colName = songNode.ChildNodes.Item(i).Name;
                        dt.Columns.Add(colName);
                        dt.Columns[colName].DefaultValue = songNode.ChildNodes.Item(i).InnerText.Trim();
                    }
                }
            }
            return dt;
        }


        

        #endregion

        #region 读取XML资源中指定的DataTable的指定行指定列的值
        /// <summary>
        /// 读取XML资源中指定的DataTable的指定行指定列的值
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colName">列名</param>
        /// <returns>值，不存在时返回Null</returns>
        public static object GetTableCell(string source, XmlType xmlType, string tableName, int rowIndex, string colName)
        {
            DataSet ds = new DataSet();
            if (xmlType == XmlType.File)
            {
                ds.ReadXml(source);
            }
            else
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(source);
                XmlNodeReader xnr = new XmlNodeReader(xd);
                ds.ReadXml(xnr);
            }

            return ds.Tables[tableName].Rows[rowIndex][colName];
        }

        #endregion

        #region 读取XML资源中指定的DataTable的指定行指定列的值
        /// <summary>
        /// 读取XML资源中指定的DataTable的指定行指定列的值
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <returns>值，不存在时返回Null</returns>
        public static object GetTableCell(string source, XmlType xmlType, string tableName, int rowIndex, int colIndex)
        {
            DataSet ds = new DataSet();
            if (xmlType == XmlType.File)
            {
                ds.ReadXml(source);
            }
            else
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(source);
                XmlNodeReader xnr = new XmlNodeReader(xd);
                ds.ReadXml(xnr);
            }

            return ds.Tables[tableName].Rows[rowIndex][colIndex];
        }

        #endregion

        #region  获取一个字符串xml文档中的一个table，指定行，指定列的值
        /// <summary>
        /// 获取一个字符串xml文档中的一个table，指定行，指定列的值
        /// </summary>
        /// <param name="xml_string">含有xml信息的字符串</param>
        /// <param name="tablename">表名</param>
        /// <param name="row_index">指定行</param>
        /// <param name="col_name">指定列名</param>
        /// <returns>相应节点的值</returns>
        public static string get_XmlValue(string xml_string, string tablename, int row_index, string col_name)
        {
            System.Xml.XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml_string);
            XmlElement root = xd.DocumentElement;
            XmlNode xe = root.CloneNode(false);
            xe = root.SelectNodes(tablename).Item(row_index);
            string val = "";
            if (null == xe)
            {
                return "";
            }
            foreach (XmlNode xn in xe.ChildNodes)
            {
                if (xn.LocalName == col_name)
                {
                    val = xn.InnerText;
                    break;
                }
            }
            return val;
        }

        /// <summary>
        /// 获取一个xml文件中的一个table，指定行，指定列的值
        /// </summary>
        /// <param name="xml_string">含有xml信息的字符串</param>
        /// <param name="tablename">表名</param>
        /// <param name="row_index">指定行</param>
        /// <param name="col_name">指定列名</param>
        /// <returns>相应节点的值</returns>
        public static string get_XmlValue(string xml_string, string tablename, int row_index, string col_name, bool isfile)
        {
            System.Xml.XmlDocument xd = new XmlDocument();
            if (isfile)
                xd.Load(xml_string);
            else
                xd.LoadXml(xml_string);
            XmlElement root = xd.DocumentElement;
            XmlNode xe = root.CloneNode(false);
            xe = root.SelectNodes(tablename).Item(row_index);
            string val = "";
            if (null == xe)
            {
                return "";
            }
            foreach (XmlNode xn in xe.ChildNodes)
            {
                if (xn.LocalName == col_name)
                {
                    val = xn.InnerText;
                    break;
                }
            }
            return val;
        }

        #endregion

        #region 获取一个字符串xml文档中的dt
        ///<remarks>
        ///
        ///</remarks>
        /// <summary>
        /// 获取一个字符串xml文档中的dt
        /// </summary>
        /// <param name="xml_string">含有xml信息的字符串</param>
        public static void get_XmlValue_dt(string xml_string, ref DataTable dt, string table_name)
        {
            DataSet ds = new DataSet();
            System.Xml.XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml_string);
            //System.IO.FileStream fs = new System.IO.FileStream("c:\\aa.xml",System.IO.FileMode.Open);

            //xd.Save(fs);
            //                        System.Xml.XmlReader xr=(System.Xml.XmlReader)((object)sr);
            //ds=new DataSet();
            XmlNodeReader xnr = new XmlNodeReader(xd);
            ds.ReadXml(xnr);
            xnr.Close();
            dt = ds.Tables[table_name];
        }
        #endregion

        #region  将DataTable写入XML文件中
        /// <summary>
        /// 将DataTable写入XML文件中
        /// </summary>
        /// <param name="dt">含有数据的DataTable</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveTableToFile(DataTable dt, string filePath)
        {
            DataSet ds = new DataSet("Config");
            ds.Tables.Add(dt.Copy());

            ds.WriteXml(filePath);
        }
        #endregion

        #region  将DataTable以指定的根结点名称写入文件
        /// <summary>
        /// 将DataTable以指定的根结点名称写入文件
        /// </summary>
        /// <param name="dt">含有数据的DataTable</param>
        /// <param name="rootName">根结点名称</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveTableToFile(DataTable dt, string rootName, string filePath)
        {
            DataSet ds = new DataSet(rootName);
            ds.Tables.Add(dt.Copy());

            ds.WriteXml(filePath);
        }
        #endregion

        #region 使用DataSet方式更新XML文件节点

        /// <summary>
        /// 使用DataSet方式更新XML文件节点
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="tableName">表名称</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colName">列名</param>
        /// <param name="content">更新值</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateTableCell(string filePath, string tableName, int rowIndex, string colName, string content)
        {
            bool flag = false;

            DataSet ds = new DataSet();
            ds.ReadXml(filePath);
            DataTable dt = ds.Tables[tableName];

            if (dt.Rows[rowIndex][colName] != null)
            {
                dt.Rows[rowIndex][colName] = content;
                ds.WriteXml(filePath);
                flag = true;
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        #endregion

        #region  使用DataSet方式更新XML文件节点
        /// <summary>
        /// 使用DataSet方式更新XML文件节点
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="tableName">表名称</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <param name="content">更新值</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateTableCell(string filePath, string tableName, int rowIndex, int colIndex, string content)
        {
            bool flag = false;

            DataSet ds = new DataSet();
            ds.ReadXml(filePath);
            DataTable dt = ds.Tables[tableName];

            if (dt.Rows[rowIndex][colIndex] != null)
            {
                dt.Rows[rowIndex][colIndex] = content;
                ds.WriteXml(filePath);
                flag = true;
            }
            else
            {
                flag = false;
            }

            return flag;
        }
        #endregion

        #region 读取XML资源中的指定节点内容

        /// <summary>
        /// 读取XML资源中的指定节点内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>节点内容</returns>
        public static object GetNodeValue(string source, XmlType xmlType, string nodeName)
        {
            XmlDocument xd = new XmlDocument();
            if (xmlType == XmlType.File)
            {
                xd.Load(source);
            }
            else
            {
                xd.LoadXml(source);
            }
            XmlElement xe = xd.DocumentElement;
            XmlNode xn = xe.SelectSingleNode("//" + nodeName);
            if (xn != null)
            {
                return xn.InnerText;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 读取XML资源中的指定节点内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>节点内容</returns>
        public static object GetNodeValue(string source, string nodeName)
        {
            if (source == null || nodeName == null || source == "" || nodeName == "" || source.Length < nodeName.Length * 2)
            {
                return null;
            }
            else
            {
                int start = source.IndexOf("<" + nodeName + ">") + nodeName.Length + 2;
                int end = source.IndexOf("</" + nodeName + ">");
                if (start == -1 || end == -1)
                {
                    return null;
                }
                else if (start >= end)
                {
                    return null;
                }
                else
                {
                    return source.Substring(start, end - start);
                }
            }
        }


        #endregion

        #region 更新XML文件中的指定节点内容
        /// <summary>
        /// 更新XML文件中的指定节点内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeValue">更新内容</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateNode(string filePath, string nodeName, string nodeValue)
        {
            bool flag = false;

            XmlDocument xd = new XmlDocument();
            xd.Load(filePath);
            XmlElement xe = xd.DocumentElement;
            XmlNode xn = xe.SelectSingleNode("//" + nodeName);
            if (xn != null)
            {
                xn.InnerText = nodeValue;
                flag = true;
            }
            else
            {
                flag = false;
            }
            xd.Save(filePath);

            return flag;
        }

        public static bool UpdateNode(string filePath, string nodeName, SysConfigModel NodeModel)
        {
            bool flag = false;

            XmlDocument xd = new XmlDocument();
            xd.Load(filePath);
            XmlElement xe = xd.DocumentElement;
            XmlNode xn = xe.SelectSingleNode("//" + nodeName);
            if (xn != null)
            {
                xn.InnerText = NodeModel.Value;
                xn.Attributes["title"].InnerText = NodeModel.Title;
                xn.Attributes["descrption"].InnerText = NodeModel.Descrption;
                flag = true;
            }
            else
            {
                flag = false;
            }
            xd.Save(filePath);

            return flag;
        }
        #endregion

        #region 操作xml文件中指定节点的数据
        /// <summary>
        /// 获得xml文件中指定节点的节点数据
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static string GetNodeInfoByNodeName(string path, string nodeName)
        {
            string XmlString = "";
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            System.Xml.XmlElement root = xml.DocumentElement;
            System.Xml.XmlNode node = root.SelectSingleNode("//" + nodeName);
            if (node != null)
            {
                XmlString = node.InnerText;
            }
            return XmlString;
        }
        #endregion

        
        /// <summary>
        /// 根据xml路径获取DataSet。如果Table名为空：flag=false 返回所有xml的数据；flag=true 将xml中的table名作为数据项返回。否则根据table名获取相应的table信息返回。
        /// </summary>
        /// <param name="XmlPath">xml文件路径</param>
        /// <param name="TableName">所要获取的Table名，可为空</param>
        /// <param name="flag">若为true，则只将所有表名作为数据项返回；若为false，则返回所要获取的Table的所有数据</param>
        /// <returns>返回所获取的DataSet</returns>
        public static DataSet GetTableByXml(string XmlPath, string TableName, bool flag)
        {
            DataSet ds = new DataSet();
            if (TableName == "")
            {
                DataSet ds1 = new DataSet();
                ds1.ReadXml(XmlPath);
                if (ds1.Tables.Count > 0)
                {
                    if (flag)
                    {
                        DataTable dt = new DataTable("typeTable");
                        dt.Columns.Add("TableName", typeof(string));
                        ds.Tables.Add(dt);
                        for (int i = 0; i < ds1.Tables.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["TableName"] = ds1.Tables[i].TableName;
                            ds.Tables["typeTable"].Rows.Add(dr);
                        }
                    }
                    else
                    {
                        ds = ds1.Copy();
                    }
                }
            }
            else
            {
                DataSet ds2 = new DataSet();
                ds2.ReadXml(XmlPath);
                if (ds2.Tables[TableName] != null)
                {
                    ds.Tables.Add(ds2.Tables[TableName].Copy());
                }
            }
            return ds;
        }
        /// <summary>
        /// escape invalid Unicode in XML
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Replaceinvalid(string str)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[\x00-\x08|\x0b-\x0c|\x0e-\x1f]");
            return r.Replace(str, " ");
        }

        /// <summary>
        /// 获得接口错误信息
        /// </summary>
        /// <param name="errCode">错误编码</param>
        /// <returns></returns>
        public static string GetInterfaceErrorString(string errCode)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"GB2312\"?>");
            sb.Append("<Root>");
            sb.Append("<Result><return_result>" + errCode + "</return_result></Result>");
            sb.Append("</Root>");

            return sb.ToString();
        }
    }
}
