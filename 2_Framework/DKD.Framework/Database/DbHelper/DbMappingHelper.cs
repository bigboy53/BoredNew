using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DKD.Framework.Database.DbHelper
{
    public abstract class DbMappingHelper
    {

        protected Type _classType;
        protected static Dictionary<Type, Dictionary<string, string>> _MappingField = new Dictionary<Type, Dictionary<string, string>>();
        protected static Dictionary<Type, string> _MappingTable = new Dictionary<Type, string>();

    }

   /// <summary>
    /// 数据实体与数据库字段映射帮助类
   /// </summary>
   /// <typeparam name="T"></typeparam>
    public class DbMappingHelper<T> : DbMappingHelper
    {
        private static readonly Lazy<DbMappingHelper<T>> _Instance = new Lazy<DbMappingHelper<T>>(() =>
        {
            return new DbMappingHelper<T>();
        });
        /// <summary>
        /// 实例
        /// </summary>
        public static DbMappingHelper<T> Instance
        {
            get
            {
                return _Instance.Value;
                //return new DbMappingHelper<T>();
            }
        }
        /// <summary>
        /// 注册表名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DbMappingHelper<T> RegistTable(string tableName)
        {
            var ty = typeof(T);
            if (!_MappingTable.ContainsKey(ty))
            {
                _MappingTable.Add(ty, tableName);
            }
            return this;
        }
        /// <summary>
        /// 注册实体属性与数据库字段名关系
        /// </summary>
        /// <param name="expr">属性名</param>
        /// <param name="columnName">字段名</param>
        public DbMappingHelper<T> RegistField(Expression<Func<T, object>> expr, string columnName)
        {
            var ty = typeof(T);
            var propertyName = "";
            if (!_MappingField.ContainsKey(ty))
            {
                _MappingField.Add(ty, new Dictionary<string, string>());
            }
            propertyName = GetProperty(expr);
            _MappingField[ty].Add(propertyName, columnName);
            return this;
        }
        /// <summary>
        /// 获取实体数据库字段名
        /// </summary>
        /// <param name="expr">属性名</param>
        /// <returns></returns>
        public string GetField(Expression<Func<T, object>> expr)
        {
            var field = "";
            var ty = typeof(T);
            field = GetProperty(expr);
            if (_MappingField.ContainsKey(ty) && _MappingField[ty].ContainsKey(field))
            {
                field = _MappingField[ty][field];
            }
            return field;
        }
        /// <summary>
        /// 获取实体数据库字段名
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public string GetFieldByName(string propertyName)
        {
            var field = propertyName;
            var ty = typeof(T);
            if (_MappingField.ContainsKey(ty) && _MappingField[ty].ContainsKey(field))
            {
                field = _MappingField[ty][field];
            }
            return field;
        }
        /// <summary>
        /// 获取实体对应的数据库表名
        /// </summary>
        /// <returns></returns>
        public string GetTable()
        {
            var ty = typeof(T);
            if (_MappingTable.ContainsKey(ty))
            {
                return _MappingTable[ty];
            }
            return ty.Name;
        }
        private string GetProperty(Expression<Func<T, object>> expr)
        {
            var field = "";
            if (expr.Body is UnaryExpression)
            {
                var uy = expr.Body as UnaryExpression;
                field = (uy.Operand as MemberExpression).Member.Name;
            }
            else
            {
                field = (expr.Body as MemberExpression).Member.Name;
            }
            return field;
        }
    }
}
