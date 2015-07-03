using System;
using System.Collections.Generic;
using System.Xml;
using System.Reflection;

namespace DKD.Core.Config.Xml
{
    /// <summary>
    ///作用： 系统配置文件XML帮助类 功能类
    /// </summary>
    public class SystemConfigurationHelper
    {
        /// <summary>
        /// 文件
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 根节点
        /// </summary>
        public string RootName { get; set; }
        /// <summary>
        /// 标识名字
        /// </summary>
        public string MarkName { get; set; }
        /// <summary>
        /// 对象节点名字
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        ///  行对象 InnerText 值保存的属性名称
        /// </summary>
        public string VaueName { get; set; }


        #region "对象格式 存在增删改查"
        /// <summary>
        /// 分页获取XML数据根据反射
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<T> GetListByPage<T>(int pagesize, int pageIndex, out int dataCount)
        {
            XmlDocument xml = new XmlDocument();
            //xml.Load(System.Web.HttpContext.Current.Server.MapPath(Path));
            xml.Load(Path);
            XmlNode node = xml.SelectSingleNode(RootName);
            XmlNodeList chNode = node.ChildNodes;
            //计算页码
            if (chNode.Count % pagesize == 0)
            {
                dataCount = chNode.Count / pagesize;
            }
            else
            {
                dataCount = (chNode.Count - chNode.Count % pagesize) / pagesize + 1;
            }

            List<T> list = new List<T>();
            int staInt = pagesize * (pageIndex - 1);
            int endInt = pagesize * pageIndex;
            int commentNum = 0;
            for (int i = 0; i < chNode.Count; i++)
            {

                if (chNode[i].NodeType == XmlNodeType.Comment)
                {
                    commentNum += 1;
                    continue;
                }
                if (i - commentNum >= staInt && i - commentNum < endInt)
                {
                    list.Add(GetModelByNode<T>(chNode[i]));
                }
            }
            //foreach (XmlNode item in chNode)
            //{
            //    list.Add(GetModelByNode<T>(item));
            //}
            return list;
        }

        /// <summary>
        /// 根据一个节点返回实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        private T GetModelByNode<T>(XmlNode node)
        {
            T obj = (T)Assembly.Load((typeof(T)).Namespace).CreateInstance(typeof(T).FullName);
            PropertyInfo[] fields = obj.GetType().GetProperties();
            XmlNodeList sunList = node.ChildNodes;
            foreach (XmlNode sunItem in sunList)
            {
                foreach (PropertyInfo PI in fields)
                {
                    Type InfoType = PI.PropertyType;
                    if (sunItem.Name.Equals(PI.Name))
                    {
                        #region "int"
                        if ((InfoType == typeof(int?) || InfoType == typeof(int)))
                        {
                            PI.SetValue(obj, Convert.ToInt32(sunItem.InnerText), null);
                        }
                        #endregion

                        #region "String"
                        if (InfoType == Type.GetType("System.String"))
                        {
                            PI.SetValue(obj, sunItem.InnerText, null);
                        }
                        #endregion

                        #region "Double"
                        if ((InfoType == typeof(double?) || InfoType == typeof(double)))
                        {
                            PI.SetValue(obj, Convert.ToDouble(sunItem.InnerText), null);
                        }
                        #endregion

                        #region "Decimal"
                        if ((InfoType == typeof(decimal?) || InfoType == typeof(decimal)))
                        {
                            PI.SetValue(obj, Convert.ToDecimal(sunItem.InnerText), null);
                        }
                        #endregion

                        #region "DataTime"
                        if ((InfoType == typeof(DateTime?) || InfoType == typeof(DateTime)))
                        {
                            PI.SetValue(obj, Convert.ToDateTime(sunItem.InnerText), null);
                        }
                        #endregion

                        #region "bool"
                        if ((InfoType == typeof(bool?) || InfoType == typeof(bool)))
                        {

                            if (sunItem.InnerText == "true" || sunItem.InnerText == "1")
                            {
                                PI.SetValue(obj, true, null);
                            }
                            if (sunItem.InnerText == "false" || sunItem.InnerText == "0")
                            {
                                PI.SetValue(obj, false, null);
                            }
                        }
                        #endregion

                    }
                }
            }

            return obj;
        }
        /// <summary>
        /// 根据数据模型修改XML数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddXmlByModel<T>(T model)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                //xml.Load(System.Web.HttpContext.Current.Server.MapPath(Path));
                xml.Load(Path);
                XmlNode node = xml.SelectSingleNode(RootName);
                // string nodeName = model.GetType().Name;
                string nodeName = ModelName;
                XmlNode newNode = xml.CreateElement(nodeName);

                PropertyInfo[] fields = model.GetType().GetProperties();
                foreach (PropertyInfo PI in fields)
                {
                    string Name = PI.Name;
                    string value = string.Empty;
                    if (Name.Equals(MarkName))
                    {
                        value = GetMarkValue().ToString();
                    }
                    else
                    {
                        value = PI.GetValue(model, null).ToString();
                    }
                    XmlNode newSunNode = xml.CreateElement(Name);
                    newSunNode.InnerText = value;
                    newNode.AppendChild(newSunNode);
                }
                node.AppendChild(newNode);
                //xml.Save(System.Web.HttpContext.Current.Server.MapPath(Path));
                xml.Save(Path);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 将对象添加到XML 对markID不自增长
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddXmlByModelAndNode<T>(T model)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                //xml.Load(System.Web.HttpContext.Current.Server.MapPath(Path));
                xml.Load(Path);
                XmlNode node = xml.SelectSingleNode(RootName);
                // string nodeName = model.GetType().Name;
                string nodeName = ModelName;
                XmlNode newNode = xml.CreateElement(nodeName);

                PropertyInfo[] fields = model.GetType().GetProperties();
                foreach (PropertyInfo PI in fields)
                {
                    string Name = PI.Name;
                    string value = string.Empty;
                    value = PI.GetValue(model, null).ToString();
                    XmlNode newSunNode = xml.CreateElement(Name);
                    newSunNode.InnerText = value;
                    newNode.AppendChild(newSunNode);
                }
                node.AppendChild(newNode);
                //xml.Save(System.Web.HttpContext.Current.Server.MapPath(Path));
                xml.Save(Path);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 修改XML 根据数据模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateXmlByModel<T>(T model)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath(Path));
            xmlDoc.Load(Path);
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(MarkName);
            XmlNode node = null;
            for (int i = 0; i < xmlList.Count; i++)
            {

                PropertyInfo[] fields = model.GetType().GetProperties();
                foreach (PropertyInfo PI in fields)
                {
                    if (PI.Name == MarkName && PI.GetValue(model, null).ToString().Equals(xmlList[i].InnerText))
                    {
                        node = xmlList[i].ParentNode;
                    }
                }
            }
            if (node != null)
            {
                XmlNodeList xmlSunList = node.ChildNodes;
                for (int i = 0; i < xmlSunList.Count; i++)
                {
                    PropertyInfo[] fields = model.GetType().GetProperties();
                    foreach (PropertyInfo PI in fields)
                    {
                        if (PI.Name.Equals(xmlSunList[i].Name))
                        {

                            xmlSunList[i].InnerText = PI.GetValue(model, null).ToString();
                        }
                    }
                }
                //xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath(Path));
                xmlDoc.Save(Path);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除数据根据模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteXmlByModel<T>(T model)
        {
            XmlDocument xmlDoc = new XmlDocument();
            // xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath(Path));
            xmlDoc.Load(Path);
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(MarkName);
            XmlNode node = null;
            for (int i = 0; i < xmlList.Count; i++)
            {

                PropertyInfo[] fields = model.GetType().GetProperties();
                foreach (PropertyInfo PI in fields)
                {
                    if (PI.Name == MarkName && PI.GetValue(model, null).ToString().Equals(xmlList[i].InnerText))
                    {
                        node = xmlList[i].ParentNode;
                    }
                }
            }
            if (node != null)
            {
                node.ParentNode.RemoveChild(node);
                // xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath(Path));
                xmlDoc.Save(Path);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteXmlByNodeValue(object value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            // xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath(Path));
            xmlDoc.Load(Path);
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(MarkName);
            XmlNode node = null;
            for (int i = 0; i < xmlList.Count; i++)
            {
                if (xmlList[i].InnerText.Equals(value.ToString()))
                {
                    node = xmlList[i].ParentNode;
                }
            }
            if (node != null)
            {
                node.ParentNode.RemoveChild(node);
                // xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath(Path));
                xmlDoc.Save(Path);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取主键标识值
        /// </summary>
        /// <returns></returns>
        private int GetMarkValue()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath(Path)); 
            xmlDoc.Load(Path);
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(MarkName);

            return Convert.ToInt32(xmlList[xmlList.Count - 1].InnerText) + 1;
        }

        /// <summary>
        /// 获取MarkName对应的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Verification(string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            // xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath(Path));
            xmlDoc.Load(Path);
            int rowNum = 0;
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(MarkName);
            for (int i = 0; i < xmlList.Count; i++)
            {
                if (xmlList[i].InnerText.Equals(value))
                {
                    rowNum += 1;
                }
            }
            return rowNum;
        }
        /// <summary>
        /// 获取对应的节点是否拥有该Value
        /// </summary>
        /// <param name="veriName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Verification(string veriName, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            // xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath(Path));
            xmlDoc.Load(Path);
            int rowNum = 0;
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(veriName);
            for (int i = 0; i < xmlList.Count; i++)
            {
                if (xmlList[i].InnerText.Equals(value))
                {
                    rowNum += 1;
                }
            }
            return rowNum;
        }
        public T GetModelByMarkValue<T>(string markValue)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Path);
            XmlNode node = xml.SelectSingleNode(RootName);
            XmlNodeList chNode = node.ChildNodes;
            XmlNode returnNode = null;
            for (int i = 0; i < chNode.Count; i++)
            {
                XmlNodeList sunList = chNode[i].ChildNodes;
                for (int y = 0; y < sunList.Count; y++)
                {
                    if (sunList[y].Name == MarkName && sunList[y].InnerText == markValue)
                    {
                        returnNode = chNode[i];
                    }
                }
            }
            if (returnNode != null)
            {
                return GetModelByNode<T>(returnNode);
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// 根据节点名字与值返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        /// <returns></returns>
        public T GetModelByNode<T>(string nodeName, string nodeValue)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Path);
            XmlNode node = xml.SelectSingleNode(RootName);
            XmlNodeList chNode = node.ChildNodes;
            XmlNode returnNode = null;
            for (int i = 0; i < chNode.Count; i++)
            {
                XmlNodeList sunList = chNode[i].ChildNodes;
                for (int y = 0; y < sunList.Count; y++)
                {
                    if (sunList[y].Name == nodeName && sunList[y].InnerText == nodeValue)
                    {
                        returnNode = chNode[i];
                    }
                }
            }
            if (returnNode != null)
            {
                return GetModelByNode<T>(returnNode);
            }
            else
            {
                return default(T);
            }
        }
        public IList<T> GetAllModelByNode<T>()
        {
            var list = new List<T>();
            XmlDocument xml = new XmlDocument();
            xml.Load(Path);
            XmlNode node = xml.SelectSingleNode(RootName);
            XmlNodeList chNode = node.ChildNodes;
            XmlNode returnNode = null;
            for (int i = 0; i < chNode.Count; i++)
            {
                returnNode = chNode[i];
                if(returnNode !=null)
                {
                    if (returnNode.Name != "#comment")
                    {
                        list.Add(GetModelByNode<T>(returnNode));
                    }
                }
            }
            
            return list;
        }
        #endregion


        #region "行格式 存在查和改"
        public List<T> GetListByPageRow<T>(int pageSize, int PageIndex, out int Count)
        {

            XmlDocument xml = new XmlDocument();
            //xml.Load(System.Web.HttpContext.Current.Server.MapPath(Path));
            xml.Load(Path);
            XmlNodeList xmlList = xml.GetElementsByTagName(ModelName);
            if (xmlList.Count == 1)
            {
                XmlNodeList chNode = xmlList[0].ChildNodes;

                if (chNode.Count % pageSize == 0)
                {
                    Count = chNode.Count / pageSize;
                }
                else
                {
                    Count = (chNode.Count - chNode.Count % pageSize) / pageSize + 1;
                }
                List<T> list = new List<T>();
                int staInt = pageSize * (PageIndex - 1);
                int endInt = pageSize * PageIndex;
                int commentNum = 0;
                for (int i = 0; i < chNode.Count; i++)
                {
                    if (chNode[i].NodeType == XmlNodeType.Comment)
                    {
                        commentNum += 1;
                        continue;
                    }
                    if (i - commentNum >= staInt && i - commentNum < endInt)
                    {
                        //list.Add(GetModelByNode<T>(chNode[i]));
                        list.Add(GetModelByXmlRow<T>(chNode[i]));
                    }
                }
                return list;

            }
            else
            {
                Count = 0;
                return null;
            }
        }

        private T GetModelByXmlRow<T>(XmlNode node)
        {
            T obj = (T)Assembly.Load((typeof(T)).Namespace).CreateInstance(typeof(T).FullName);
            PropertyInfo[] fields = obj.GetType().GetProperties();
            XmlAttributeCollection nodeCn = node.Attributes;
            for (int i = 0; i < nodeCn.Count; i++)
            {
                foreach (PropertyInfo PI in fields)
                {
                    string name = PI.Name;

                    if (nodeCn[i].Name.ToLower().Equals(PI.Name.ToLower()))
                    {
                        Type InfoType = PI.PropertyType;

                        #region "int"
                        if ((InfoType == typeof(int?) || InfoType == typeof(int)))
                        {
                            PI.SetValue(obj, Convert.ToInt32(nodeCn[i].Value), null);
                        }
                        #endregion

                        #region "String"
                        if (InfoType == Type.GetType("System.String"))
                        {
                            PI.SetValue(obj, nodeCn[i].Value, null);
                        }
                        #endregion
                        #region "Double"
                        if ((InfoType == typeof(double?) || InfoType == typeof(double)))
                        {
                            PI.SetValue(obj, Convert.ToDouble(nodeCn[i].Value), null);
                        }
                        #endregion

                        #region "Decimal"
                        if ((InfoType == typeof(decimal?) || InfoType == typeof(decimal)))
                        {
                            PI.SetValue(obj, Convert.ToDecimal(nodeCn[i].Value), null);
                        }
                        #endregion

                        #region "DataTime"
                        if ((InfoType == typeof(DateTime?) || InfoType == typeof(DateTime)))
                        {
                            PI.SetValue(obj, Convert.ToDateTime(nodeCn[i].Value), null);
                        }
                        #endregion

                        #region "bool"
                        if ((InfoType == typeof(bool?) || InfoType == typeof(bool)))
                        {

                            if (nodeCn[i].Value == "true" || nodeCn[i].Value == "1")
                            {
                                PI.SetValue(obj, true, null);
                            }
                            if (nodeCn[i].Value == "false" || nodeCn[i].Value == "0")
                            {
                                PI.SetValue(obj, false, null);
                            }
                        }
                        #endregion
                    }

                    if (PI.Name.Equals(VaueName))
                    {
                        PI.SetValue(obj, node.InnerText, null);
                    }
                }

            }
            return obj;
        }


        public bool SaveModelByXml<T>(T model)
        {
            XmlDocument xml = new XmlDocument();
            //xml.Load(System.Web.HttpContext.Current.Server.MapPath(Path));
            xml.Load(Path);
            XmlNodeList xmlList = xml.GetElementsByTagName(ModelName);
            XmlNode node = null;
            if (xmlList.Count == 1)
            {
                XmlNodeList chNode = xmlList[0].ChildNodes;
                for (int i = 0; i < chNode.Count; i++)
                {
                    XmlAttributeCollection nodeCn = chNode[i].Attributes;
                    if (nodeCn == null) continue;
                    for (int y = 0; y < nodeCn.Count; y++)
                    {
                        if (nodeCn[y].Name.Equals(MarkName))
                        {
                            PropertyInfo[] fields = model.GetType().GetProperties();
                            foreach (PropertyInfo PI in fields)
                            {
                                if (PI.Name == MarkName && PI.GetValue(model, null).ToString().Equals(nodeCn[y].Value))
                                {
                                    node = chNode[i];
                                }
                            }
                        }
                    }
                }
                XmlAttributeCollection UpdateNodeCh = node.Attributes;
                for (int i = 0; i < UpdateNodeCh.Count; i++)
                {
                    PropertyInfo[] fields = model.GetType().GetProperties();
                    foreach (PropertyInfo PI in fields)
                    {
                        if (PI.Name.ToLower().Equals(UpdateNodeCh[i].Name.ToLower()))
                        {
                            UpdateNodeCh[i].Value = PI.GetValue(model, null).ToString();
                        }
                        if (PI.Name == VaueName)
                        {
                            node.InnerText = PI.GetValue(model, null).ToString();
                        }
                    }
                }
                xml.Save(Path);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据标识属性值 返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="markValue"></param>
        /// <returns></returns>
        public T GetModelByMarkID<T>(string markValue)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Path);
            XmlNodeList xmlList = xml.GetElementsByTagName(ModelName);
            if (xmlList.Count == 1)
            {
                XmlNode node = null;
                XmlNodeList chNode = xmlList[0].ChildNodes;
                for (int i = 0; i < chNode.Count; i++)
                {
                    XmlAttributeCollection nodeCn = chNode[i].Attributes;
                    if(nodeCn==null)continue;
                    for (int y = 0; y < nodeCn.Count; y++)
                    {
                        if (nodeCn[y].Name.ToLower() == MarkName.ToLower() && nodeCn[y].Value == markValue)
                        {
                            node = chNode[i];
                        }
                    }
                }
                if (node != null)
                {

                    return GetModelByXmlRow<T>(node);
                }
                else
                {
                    return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// 根据节点名字与值返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        /// <returns></returns>
        public T GetModelByRow<T>(string nodeName, string nodeValue)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Path);
            XmlNodeList xmlList = xml.GetElementsByTagName(ModelName);
            if (xmlList.Count == 1)
            {
                XmlNode node = null;
                XmlNodeList chNode = xmlList[0].ChildNodes;
                for (int i = 0; i < chNode.Count; i++)
                {
                    XmlAttributeCollection nodeCn = chNode[i].Attributes;
                    for (int y = 0; y < nodeCn.Count; y++)
                    {
                        if (nodeCn[y].Name.ToLower() == nodeName.ToLower() && nodeCn[y].Value == nodeValue)
                        {
                            node = chNode[i];
                        }
                    }
                }
                if (node != null)
                {

                    return GetModelByXmlRow<T>(node);
                }
                else
                {
                    return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }
        #endregion
    }
}