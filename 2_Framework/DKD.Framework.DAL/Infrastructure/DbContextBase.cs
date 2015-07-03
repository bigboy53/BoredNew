using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DKD.Framework.Contract;
using DKD.Framework.Contract.Auditable;
using EntityFramework.Extensions;
using PageHelper;

namespace DKD.Framework.Data.Infrastructure
{
    public class DbContextBase : DbContext, IDisposable
    {
        public DbContextBase(string connectionStr)
            : base(connectionStr)
        { }

        private IAuditable _auditable;

        public DbContextBase(string connectionStr, IAuditable auditable)
            : base(connectionStr)
        {
            _auditable = auditable;
        }

        #region 查询
        public IEnumerable<T> GetList<T>() where T : BaseModel
        {
            return this.Set<T>().ToList();
        }

        public IEnumerable<T> GetList<T>(Func<T, bool> where) where T : BaseModel
        {

            if (where == null)
                return this.Set<T>().ToList();
            return this.Set<T>().Where(where).ToList();
        }

        public int GetCount<T>(Func<T, bool> where) where T : BaseModel
        {

            return this.Set<T>().Where(where).ToList().Count();
        }

        public PageData GetPage<T>(int pageIndex, int pageSize, Func<T, object> order, OrderType orderType,
            Func<T, bool> where) where T : BaseModel
        {
            var pageData = new PageData();
            if (where == null)
            {
                pageData.DataCount = this.Set<T>().Count();
                if (orderType == OrderType.Asc) //升序排列
                    pageData.Data =
                        this.Set<T>()
                            .OrderBy(order)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
                else
                    pageData.Data =
                        this.Set<T>()
                            .OrderByDescending(order)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            }
            else
            {
                pageData.DataCount = this.Set<T>().Count(@where);
                if (orderType == OrderType.Asc) //升序排列
                    pageData.Data =
                        this.Set<T>()
                            .Where(@where)
                            .OrderBy(order)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
                else
                    pageData.Data =
                        this.Set<T>()
                            .Where(@where)
                            .OrderByDescending(order)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            }
            return pageData;
        }

        public T GetModel<T>(Func<T, bool> exp) where T : BaseModel
        {

            return this.Set<T>().Where(exp).SingleOrDefault();
        }
        #endregion

        #region 操作
        public int Insert<T>(T entity) where T : BaseModel
        {
            this.Set<T>().Add(entity);
            return this.SaveChanges();
        }

        public bool Insert<T>(List<T> list) where T : BaseModel
        {
            this.Set<T>().AddRange(list);
            return this.SaveChanges() > 0;
        }

        public bool Update<T>(T entity) where T : BaseModel
        {
            var obj = this.Set<T>();
            obj.Attach(entity);
            this.Entry(entity).State = EntityState.Modified;
            return this.SaveChanges() > 0;
        }

        public bool Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T : BaseModel
        {
            if (where == null || entity == null)
                return false;
            return this.Set<T>().Where(where).Update(entity) > 0;
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete<T>(T entity) where T : BaseModel
        {
            var obj = this.Set<T>();
            if (entity != null)
            {
                obj.Attach(entity);
                this.Entry(entity).State = EntityState.Deleted;
                obj.Remove(entity);
                return this.SaveChanges() > 0;
            }
            return false;
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Delete<T>(Expression<Func<T, bool>> where) where T : BaseModel
        {
            return this.Set<T>().Delete(where) > 0;
        }

        public bool Exist<T>(Func<T, bool> where) where T : BaseModel
        {
            return this.Set<T>().Any(where);
        }
        #endregion

        public override int SaveChanges()
        {
            this.WrieAuditLog();
            try
            {
                return base.SaveChanges();
            }
            catch (UpdateException exception)
            {
                throw new Exception("添加或更新错误", exception);
            }
            catch (Exception exception)
            {
                throw new Exception("SaveChanges()错误", exception);
            }
        }

        internal void WrieAuditLog()
        {
            if (this._auditable == null)
                return;

            foreach (var dbEntry in this.ChangeTracker.Entries<BaseModel>().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                var auditableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(AuditableAttribute), false).SingleOrDefault() as AuditableAttribute;
                if (auditableAttr == null)
                    continue;

                var operaterName = "";

                var entry = dbEntry;
                Task.Factory.StartNew(() =>
                {
                    var tableAttr = entry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
                    string tableName = tableAttr != null ? tableAttr.Name : entry.Entity.GetType().Name;
                    var moduleName = entry.Entity.GetType().FullName.Split('.').Skip(1).FirstOrDefault();

                    this._auditable.WriteLog(entry.Entity.ID, operaterName, moduleName, tableName,
                        entry.State.ToString(), entry.Entity);
                });
            }
        }
    }
}
