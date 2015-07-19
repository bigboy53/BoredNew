using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using AutoMapper;
using Bored.Model;
using Bored.IRepository;
using DKD.Core.Cache;
using DKD.Framework.Const;
using DKD.Framework.Data;
using DKD.Framework.Utility;
using Manage.ViewModel;

namespace Bored.Repository
{
    public class RolesRepository : RepositoryBase<Roles>, IRolesRepository
    {
        public bool Update(RolesDto model)
        {
            model.RolePermission.ForEach(t =>
            {
                t.RID = model.ID;
                t.CreateTime = DateTime.Now;
            });
            using (var db = new BoredEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    db.Entry(Mapper.Map<Roles>(model)).State = EntityState.Modified;
                    //this.DataContext.RolePermission.ExecuteStoreCommand("");
                    object[] para =
                        {
                            new SqlParameter("@RID", model.ID)
                        };
                    db.Database.ExecuteSqlCommand("DELETE dbo.RolePermission WHERE RID=@RID", para);
                    db.RolePermission.AddRange(Mapper.Map<List<RolePermission>>(model.RolePermission));
                    db.SaveChanges();
                    transaction.Complete();
                }
                CacheManager.Cache.Remove(GlobalCacheKey.RolePermission.ToFormat(model.ID));
                return true;
            }
        }

        public int Insert(RolesDto model)
        {
            var entity = Mapper.Map<Roles>(model);
            using (var db = new BoredEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    db.Roles.Add(entity);
                    db.SaveChanges();
                    model.RolePermission.ForEach(t =>
                    {
                        t.RID = entity.ID;
                        t.CreateTime = DateTime.Now;
                    });
                    db.RolePermission.AddRange(
                        Mapper.Map<List<RolePermission>>(model.RolePermission));
                    db.SaveChanges();
                    //提交事务
                    transaction.Complete();
                }
                return entity.ID;
            }
        }

        public List<RolePermissionDto> GetPermissionList(int rid)
        {
            var list = CacheManager.Cache.Get(GlobalCacheKey.RolePermission.ToFormat(rid));
            if (list != null)
                return (List<RolePermissionDto>)list;
            List<RolePermission> data;
            using (var db = new BoredEntities())
            {
                data = db.RolePermission.Where(t => t.RID == rid).ToList();
            }
            var dataList = Mapper.Map<List<RolePermissionDto>>(data);
            CacheManager.Cache.Set(GlobalCacheKey.RolePermission.ToFormat(rid), dataList);
            return dataList;
        }
    }
}
