using System;
using System.Collections.Generic;
using System.Data;
//using DKD.PackageField.Fileds;
//using DKD.PackageField.Search;
//using DKD.PackageField.Update;


namespace DKD.Framework
{

    /// <summary>
    /// 访问数据库的核心操作类
    /// </summary>
    public static class EntityCore<EntityObject> where EntityObject : class, new()
    {
        //#region 内部方法

        ///// <summary>
        ///// 调用方法
        ///// </summary>
        ///// <param name="method">要调用的方法</param>
        ///// <param name="parameter">调用方法的参数</param>
        ///// <returns></returns>
        //private static object InvokeMethod(string method, object[] parameter)
        //{
        //    var t = Database.Core.DataAccessCache.GetType<EntityObject>();
            
        //    var tInstance = Database.Core.DataAccessCache.DynamicInstanceCache[t.GUID.ToString()];

        //    var methodReturn= t.GetMethod(method).Invoke(tInstance, parameter);
        //    return methodReturn;
        //}

        ///// <summary>
        ///// 调用方法
        ///// </summary>
        ///// <param name="method">要调用的方法</param>
        ///// <param name="parameter">调用方法的参数</param>
        ///// <param name="parameterTypes">调用方法的参数类型</param>
        ///// <returns></returns>
        //private static object InvokeMethod(string method, object[] parameter, Type[] parameterTypes)
        //{
        //    var t = Database.Core.DataAccessCache.GetType<EntityObject>();

        //    object tInstance = Database.Core.DataAccessCache.DynamicInstanceCache[t.GUID.ToString()];

        //    return t.GetMethod(method, parameterTypes).Invoke(tInstance, parameter);
        //}

        //#endregion 

        //#region 对外方法


        ///// <summary>
        ///// 添加一条记录
        ///// </summary>
        ///// <param name="model">要添加的实体</param>
        ///// <returns>返回新增以int 自动增加的主键的记录号</returns>
        //public static int Add(EntityObject model)
        //{
        //    return (int)InvokeMethod("Add", new object[] { model });
        //}

        ///// <summary>
        ///// 删除记录
        ///// </summary>
        ///// <param name="primaryKeyValue">主键值</param>
        //public static bool Delete(object primaryKeyValue)
        //{
        //    return (bool)InvokeMethod("Delete", new object[] { primaryKeyValue });
        //}

        ///// <summary>
        ///// 根据条件删除记录
        ///// </summary>
        ///// <param name="search">条件</param>
        //public static bool DeleteWhere(SearchDepot<EntityObject> search)
        //{
        //    return (bool)InvokeMethod("DeleteWhere", new object[] { search });
        //}

        ///// <summary>
        ///// 查看记录是否存在
        ///// </summary>
        ///// <param name="primaryKeyValue">主键值</param>
        ///// <returns></returns>
        //public static bool Exists(object primaryKeyValue)
        //{
        //    return (bool)InvokeMethod("Exists", new object[] { primaryKeyValue });
        //}

        ///// <summary>
        ///// 查询记录
        ///// </summary>
        ///// <param name="search">条件</param>
        ///// <returns></returns>
        //public static DataSet GetList(SearchDepot<EntityObject> search)
        //{
        //    return (DataSet)InvokeMethod("GetList", new object[] { search }, new Type[] { typeof(SearchDepot) });
        //}

        ///// <summary>
        ///// 查询记录并返回指定条数
        ///// </summary>
        ///// <param name="topRecord">返回指定条数</param>
        ///// <param name="search">条件</param>
        ///// <returns></returns>
        //public static DataSet GetList(int topRecord, SearchDepot<EntityObject> search)
        //{
        //    return (DataSet)InvokeMethod("GetList", new object[] { topRecord, search }, new Type[] { typeof(int), typeof(SearchDepot) });
        //}

        ///// <summary>
        ///// 返回一个实体
        ///// </summary>
        ///// <param name="primaryKeyValue">主键值</param>
        ///// <param name="fileds"></param>
        ///// <returns></returns>
        //public static EntityObject GetModel(object primaryKeyValue,FiledsDepot<EntityObject> fileds=null)
        //{
        //    return (EntityObject)InvokeMethod("GetModel", new object[] { primaryKeyValue, fileds });
        //}

        ///// <summary>
        ///// 返回实体列表
        ///// </summary>
        ///// <param name="search">查询条件</param>
        ///// <param name="fileds">字段</param>
        ///// <returns></returns>
        //public static List<EntityObject> GetModelList(SearchDepot<EntityObject> search, FiledsDepot<EntityObject> fileds = null)
        //{
        //    return (List<EntityObject>)InvokeMethod("GetModels", new object[] { search, fileds });
        //}

        ///// <summary>
        ///// 返回实体列表
        ///// </summary>
        ///// <param name="topRecord">返回指定条数</param>
        ///// <param name="search">条件</param>
        ///// <param name="fileds">字段</param>
        ///// <returns></returns>
        //public static List<EntityObject> GetModelList(int topRecord, SearchDepot<EntityObject> search, FiledsDepot<EntityObject> fileds = null)
        //{
        //    return
        //        (List<EntityObject>)
        //            InvokeMethod("GetModelsByTop", new object[] { topRecord, search, fileds });
        //}

        ///// <summary>
        ///// 更新记录
        ///// </summary>
        ///// <param name="model">实体</param>
        //public static bool Update(EntityObject model)
        //{
        //    return (bool)InvokeMethod("Update", new object[] { model });
        //}

        ///// <summary>
        ///// 更新记录
        ///// </summary>
        ///// <param name="primaryKeyValue">主键值</param>
        ///// <param name="update">更新字段和值</param>
        //public static bool Update(object primaryKeyValue, UpdateDepot<EntityObject> update)
        //{

        //    return (bool)InvokeMethod("UpdatePart", new object[] { primaryKeyValue, update });
        //}

        ///// <summary>
        ///// 查询记录
        ///// </summary>
        ///// <param name="topRecord">返回多少条</param>
        ///// <param name="search">查询条件</param>
        ///// <param name="returnFields">返回的字段</param>
        ///// <returns>符合条件的记录</returns>
        //public static DataSet GetListAndFileds(int topRecord, SearchDepot<EntityObject> search, string returnFields)
        //{

        //    return (DataSet)InvokeMethod("GetListFieds", new object[] { topRecord, search, string.IsNullOrEmpty(returnFields) ? "*" : returnFields });
        //}

        ///// <summary>
        ///// 根据条件获取总数量
        ///// </summary>
        ///// <param name="search"></param>
        ///// <returns></returns>
        //public static int GetCount(SearchDepot<EntityObject> search)
        //{
        //    return (int)InvokeMethod("GetCount", new object[] { search }, new[] { typeof(SearchDepot<EntityObject>) });
        //}

        /////// <summary>
        /////// 更新记录
        /////// </summary>
        /////// <param name="values"></param>
        /////// <param name="search"></param>
        ////public static void Update(object values, SearchDepot<EntityObject> search)
        ////{
        ////    if (values == null)
        ////        return;

        ////    string tableName = AttributeHelper.GetInfo<EntityObject>().Table.Name;

        ////    StringBuilder sqlBuld = new StringBuilder();
        ////    sqlBuld.AppendLine(string.Format("update {0} set ", tableName));

        ////    List<System.Data.SqlClient.SqlParameter> para = new List<System.Data.SqlClient.SqlParameter>();

        ////    PropertyInfo[] propers = values.GetType().GetProperties();
        ////    for (int idx = 0; idx < propers.Length; idx++)
        ////    {
        ////        sqlBuld.AppendLine(string.Format("{1}{0}=@{0}", propers[idx].Name, idx < 1 ? "" : ","));
        ////        para.Add(new System.Data.SqlClient.SqlParameter(propers[idx].Name, propers[idx].GetValue(values, null)));
        ////    }

        ////    var searchModel = new SearchCriteria<EntityObject>(search);
        ////    string strWhere = searchModel.ToWhereClause();
        ////    if (!string.IsNullOrEmpty(strWhere))
        ////        sqlBuld.AppendLine(string.Format(" where {0}", strWhere));

        ////    DbHelperSQL.ExecuteSql(sqlBuld.ToString(), para.ToArray());

        ////}


        /////// <summary>
        /////// 更新记录
        /////// </summary>
        /////// <param name="values"></param>
        /////// <param name="search"></param>
        ////public static void UpdateSql(object values, SearchDepot<EntityObject> search)
        ////{
        ////    if (values == null)
        ////        return;

        ////    string tableName = AttributeHelper.GetInfo<EntityObject>().Table.Name;

        ////    StringBuilder sqlBuld = new StringBuilder();
        ////    sqlBuld.AppendLine(string.Format("update {0} set ", tableName));

        ////    PropertyInfo[] propers = values.GetType().GetProperties();
        ////    for (int idx = 0; idx < propers.Length; idx++)
        ////    {
        ////        sqlBuld.AppendLine(string.Format("{1}{0}={2}", propers[idx].Name, idx < 1 ? "" : ",", propers[idx].GetValue(values, null)));
        ////    }

        ////    var searchModel = new SearchCriteria<EntityObject>(search);
        ////    string strWhere = searchModel.ToWhereClause();
        ////    if (!string.IsNullOrEmpty(strWhere))
        ////        sqlBuld.AppendLine(string.Format(" where {0}", strWhere));

        ////    DbHelperSQL.ExecuteSql(sqlBuld.ToString());

        ////}

        /////// <summary>
        /////// 获取符合条件的实体
        /////// </summary>
        /////// <param name="where">查询条件</param>
        /////// <returns></returns>
        ////public static List<EntityObject> GetModelList(object where)
        ////{
        ////    string tableName = AttributeHelper.GetInfo<EntityObject>().Table.Name;

        ////    StringBuilder sqlBuld = new StringBuilder();
        ////    sqlBuld.AppendLine(string.Format("select * from {0} ", tableName));

        ////    List<System.Data.SqlClient.SqlParameter> para = new List<System.Data.SqlClient.SqlParameter>();

        ////    if (where != null)
        ////    {

        ////        PropertyInfo[] propers = where.GetType().GetProperties();
        ////        for (int idx = 0; idx < propers.Length; idx++)
        ////        {
        ////            sqlBuld.AppendLine(string.Format("{0}=@{0}", propers[idx].Name, idx < 1 ? "" : ","));
        ////            para.Add(new System.Data.SqlClient.SqlParameter(propers[idx].Name, propers[idx].GetValue(where, null)));
        ////        }
        ////    }

        ////    return DbHelperSQL.Query(sqlBuld.ToString(), para.ToArray()).Tables[0].ToList<EntityObject>();
        ////}

        //#endregion

    }
}

