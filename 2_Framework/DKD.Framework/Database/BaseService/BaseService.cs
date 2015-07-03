using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DKD.Framework.Database.DbHelper;

namespace DKD.Framework.Database.BaseService
{
    public class BaseService
    {
        public static readonly string ProcComPagination = "Com_Pagination";//多表分页查询

        /// <summary>
        /// 联合查询
        /// </summary>
        /// <param name="joinStr">表联合查询例：IP_Address as am LEFT JOIN Area as apON am.code=ap.code </param>
        /// <param name="fields">查询列必须显示指定表和列 不能为空 例：am.code,am.city </param>
        /// <param name="oderbyStr">排列字段 不能为空 列:am.ID desc</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="whereStr">查询条件</param>
        /// <param name="dataCount">总数</param>
        /// <param name="isGetTotalCount">是否查询总条数(非分页时使用)</param>
        /// <param name="groupStr">分组字段 可为空 </param>
        /// <param name="useGroup">是否使用分组，否是</param>
        /// <returns></returns>
        public IList<T> GetPageByJoin<T>(string joinStr, string fields, string oderbyStr, int pageIndex, int pageSize, string whereStr, out int dataCount,bool isGetTotalCount=true, string groupStr = "", bool useGroup=false)
        {
            if (string.IsNullOrEmpty(joinStr))
                throw new Exception("表联合查询不能为空");
            if (string.IsNullOrEmpty(fields))
                throw new Exception("查询字段不能为空");
            if (string.IsNullOrEmpty(oderbyStr))
                throw new Exception("排序不能为空");
            var totalCount = new SqlParameter { ParameterName = "@TotalCount", Direction = ParameterDirection.Output, DbType = DbType.Int32, SqlValue = 0 };
            SqlParameter[] spms =
            {
                totalCount,
                new SqlParameter("@Table",joinStr),
                new SqlParameter("@Column",fields),
                new SqlParameter("@OrderColumn",oderbyStr),
                new SqlParameter("@GroupColumn",groupStr),
                new SqlParameter("@PageSize",pageSize),
                new SqlParameter("@CurrentPage",pageIndex),
                new SqlParameter("@Group",useGroup),
                new SqlParameter("@Condition",whereStr),
                new SqlParameter("@IsGetTotleCount",isGetTotalCount)
            };
            List<T> list = new List<T>();
            var ty = typeof(T);
            var properties = ty.GetProperties();
            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(ProcComPagination, CommandType.StoredProcedure, spms))
            {
                List<string> readerCols = new List<string>();
                var readFirstFlag = true;
                while (reader.Read())
                {
                    if (readFirstFlag)
                    {
                        readFirstFlag = false;
                        readerCols = GetReaderColumns(reader);
                    }
                    var model = (T)Activator.CreateInstance(ty);
                    foreach (var property in properties)
                    {
                        if (property.CanWrite)
                        {
                            var field = DbMappingHelper<T>.Instance.GetFieldByName(property.Name);
                            if (readerCols.Contains(field) && reader[field] != DBNull.Value)
                            {
                                try
                                {
                                    property.SetValue(model, reader[field], null);
                                }
                                catch
                                {
                                    property.SetValue(model, Convert.ChangeType(reader[field], property.PropertyType), null);
                                }
                            }
                            else
                            {
                                if (property.PropertyType == typeof(String))
                                {
                                    property.SetValue(model, "", null);
                                }
                            }
                        }
                    }
                    list.Add(model);
                }

            }
            dataCount = (int)totalCount.Value;
            return list;
        }

        private static List<string> GetReaderColumns(IDataReader reader)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                list.Add(reader.GetName(i));
            }
            return list;
        }


        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="joinStr">表联合查询例：IP_Address as am LEFT JOIN Area as apON am.code=ap.code</param>
        /// <param name="fields">查询列必须显示指定表和列 不能为空 例：am.code,am.city</param>
        /// <param name="whereStr">查询条件</param>
        /// <returns></returns>
        public M GetSingleByJoin<M>(string joinStr, string fields, string whereStr)
        {
            const string oderbyStr = "1";
            int dataCount = 0;
            var data = GetPageByJoin<M>(joinStr, fields, oderbyStr, 1, 1, whereStr, out dataCount,false);
            if (data != null && data.Count > 0)
                return data.First();
            return default(M);
        }

        /// <summary>
        /// 获取Top数据
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="joinStr">表联合查询例：IP_Address as am LEFT JOIN Area as apON am.code=ap.code</param>
        /// <param name="fields">查询列必须显示指定表和列 不能为空 例：am.code,am.city</param>
        /// <param name="whereStr">查询条件</param>
        /// <param name="topCount">top条数</param>
        /// <param name="oderbyStr"></param>
        /// <param name="groupStr"></param>
        /// <returns></returns>
        public IList<M> GetTopByJoin<M>(string joinStr, string fields, string whereStr, int topCount, string oderbyStr = "1 desc", string groupStr = "")
        {
            int dataCount = 0;
            var data = GetPageByJoin<M>(joinStr, fields, oderbyStr, 1, topCount, whereStr,out dataCount,false,groupStr);
            return data;
        }

    }
}
