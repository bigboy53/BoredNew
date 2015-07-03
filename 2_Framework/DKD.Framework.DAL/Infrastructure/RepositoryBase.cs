using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DKD.Framework.Contract;
using PageHelper;

namespace DKD.Framework.Data
{
    public class RepositoryBase<T> : IDataRepository<T> where T : BaseModel
    {
        public IEnumerable<T> GetList()
        {
            using (var db = new BoredEntities())
            {
                return db.GetList<T>();
            }
        }

        public IEnumerable<T> GetList(Func<T, bool> where)
        {
            using (var db = new BoredEntities())
            {
                return db.GetList(where);
            }
        }

        public int GetCount(Func<T, bool> where)
        {
            using (var db = new BoredEntities())
            {
                return db.GetCount(where);
            }
        }

        public PageData GetPage(int pageIndex, int pageSize, Func<T, object> order, OrderType orderType,
            Func<T, bool> where)
        {
            var pageData = new PageData();
            using (var db = new BoredEntities())
            {
                pageData = db.GetPage(pageIndex, pageSize, order, orderType, where);
            }
            return pageData;
        }

        public T GetModel(Func<T, bool> exp)
        {
            using (var db = new BoredEntities())
            {
                return db.GetModel(exp);
            }

        }

        public int Insert(T entity)
        {
            using (var db = new BoredEntities())
            {
                return db.Insert(entity);
            }
        }

        public bool Insert(List<T> list)
        {
            using (var db = new BoredEntities())
            {
                return db.Insert(list);
            }
        }

        public bool Update(T entity)
        {
            using (var db = new BoredEntities())
            {
                return db.Update(entity);
            }
        }

        public bool Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            using (var db = new BoredEntities())
            {
                return db.Update(where,entity);
            }
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            using (var db = new BoredEntities())
            {
                return db.Delete(entity);
            }
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> where)
        {
            using (var db = new BoredEntities())
            {
                return db.Delete(where);
            }
        }

        public bool Exist(Func<T, bool> where)
        {
            using (var db = new BoredEntities())
            {
                return db.Exist(where);
            }
        }
    }
}
