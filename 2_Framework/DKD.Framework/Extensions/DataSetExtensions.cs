using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;

namespace DKD.Framework.Extensions
{
    /// <summary>
    /// 数据库到模型扩展
    /// </summary>
    public static class DataSetExtensions
    {

        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<TResult> ToList<TResult>(this DataTable dt) where TResult : class, new()
        {
            //创建一个属性的列表
            var prlist = new List<PropertyInfo>();

            //获取TResult的类型实例  反射的入口
            Type t = typeof(TResult);

            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });

            //创建返回的集合
            List<TResult> oblist = new List<TResult>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                TResult ob = new TResult();

                //找到对应的数据  并赋值
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });

                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;

        }




        /// <summary>
        /// 转换为一个DataTable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<TResult>(this IEnumerable<TResult> value) where TResult : class
        {

            //创建属性的集合

            List<PropertyInfo> pList = new List<PropertyInfo>();

            //获得反射的入口

            Type type = typeof(TResult);

            DataTable dt = new DataTable();

            //把所有的public属性加入到集合 并添加DataTable的列

            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });

            foreach (var item in value)
            {

                //创建一个DataRow实例

                DataRow row = dt.NewRow();

                //给row 赋值

                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));

                //加入到DataTable

                dt.Rows.Add(row);

            }

            return dt;

        }



    }
}
