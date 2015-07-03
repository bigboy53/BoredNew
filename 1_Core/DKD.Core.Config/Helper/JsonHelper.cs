using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace DKD.Core.Config.Helper
{
 
    /// <summary>
    /// 转化为json
    /// </summary>
    public class JsonHelper
    {
        #region 泛型转化为Json
        /// <summary>
        /// 泛型转化为Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonName"></param>
        /// <param name="il"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(string jsonName, IDictionary<string,T> il, int count)
        {
            StringBuilder json = new StringBuilder();
            T obj = Activator.CreateInstance<T>();
            Type type = obj.GetType();
            PropertyInfo[] pis = type.GetProperties();
            json.Append("{\"totalCount\":\"" + count.ToString() + "\",\"" + jsonName + "\":[");
            if (il.Count > 0)
            {
                int tmps = 0;
                foreach (string key in il.Keys)
                {
                    tmps++;
                    json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        json.Append("\"" + pis[j].Name + "\":\"" + pis[j].GetValue(il[key], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            json.Append(",");
                        }
                    }
                    json.Append("}");
                    if (tmps < il.Count - 1)
                    {
                        json.Append(",");
                    }
                }
            }
            json.Append("]}");
            return json.ToString();
        }

        /// <summary>
        /// 泛型转化为Json
        /// </summary>
        /// <typeparam name="T">泛型集合</typeparam>
        /// <param name="il"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(IDictionary<string,T> il)
        {
            StringBuilder json = new StringBuilder();
            T obj = Activator.CreateInstance<T>();
            Type type = obj.GetType();
            PropertyInfo[] pis = type.GetProperties();
            json.Append("[");
            if (il.Count > 0)
            {
                int tmps = 0;
                foreach (string key in il.Keys)
                {
                    tmps++;
                    json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        json.Append("\"" + pis[j].Name+ "\":\"" + pis[j].GetValue(il[key], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            json.Append(",");
                        }
                    }
                    json.Append("}");
                    if (tmps < il.Count - 1)
                    {
                        json.Append(",");
                    }
                }
            }
            json.Append("]");
            return json.ToString();
        }
        #endregion

        #region 将单个实体类转化为Json
        /// <summary>
        /// 将单个实体类转化为Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ObjectToJson<T>(T input)
        {
            StringBuilder json = new StringBuilder();
            T obj = Activator.CreateInstance<T>();
            Type type = obj.GetType();
            PropertyInfo[] pis = type.GetProperties();
            json.Append("{");
            for (int j = 0; j < pis.Length; j++)
            {
                json.Append("\"" + pis[j].Name + "\":\"" + pis[j].GetValue(input, null) + "\"");
                if (j < pis.Length - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("}");
            return json.ToString();
        }
        /// <summary>
        /// 将单个实体类转化为Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T input, string count)
        {
            StringBuilder json = new StringBuilder();
            T obj = Activator.CreateInstance<T>();
            Type type = obj.GetType();
            PropertyInfo[] pis = type.GetProperties();
            json.Append("{");
            for (int j = 0; j < pis.Length; j++)
            {
                json.Append("\"" + pis[j].Name + "\":\"" + pis[j].GetValue(input, null) + "\"");
                if (j < pis.Length - 1)
                {
                    json.Append(",");
                }
            }
            json.Append(",\"Num\":\"" + count + "\"");
            json.Append("}");
            return json.ToString();
        }
        #endregion
    }

}
