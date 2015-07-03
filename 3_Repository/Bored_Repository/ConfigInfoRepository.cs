using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bored.Model;
using Bored.IRepository;
using DKD.Core.Cache;
using DKD.Framework.Const;
using DKD.Framework.Data;
using Manage.ViewModel;
using PageHelper;

namespace Bored.Repository
{
    public class ConfigInfoRepository : RepositoryBase<ConfigInfo>, IConfigInfoRepository
    {
        public PageData GetViewPage(int pageIndex, int pageSize, int? type, string name = "")
        {
            using (var db = new BoredEntities())
            {
                var query = from c in db.ConfigInfo
                    select new ConfigInfoDto
                    {
                        ID = c.ID,
                        IsDel = c.IsDel,
                        Name = c.Name,
                        Type = c.Type,
                        CreateTime = c.CreateTime
                    };
                if (!string.IsNullOrEmpty(name))
                    query = query.Where(t => t.Name.Contains(name));
                if (type.HasValue)
                    query = query.Where(t => t.Type == type);
                query = query.Where(t => !t.IsDel);
                return query.OrderByDescending(t => t.CreateTime).ToPageList(pageIndex, pageSize);
            }
        }

        public List<ConfigInfoDto> GetAllList()
        {
            var data = CacheManager.Cache.Get(CacheKey.Config);
            if (data == null)
            {
                var list = base.GetList();
                var result = Mapper.Map<List<ConfigInfoDto>>(list);
                CacheManager.Cache.Set(CacheKey.Config, result);
                return result;
            }
            return (List<ConfigInfoDto>)data;
        }

        public string GetConfigNames(string ids)
        {
            if (string.IsNullOrEmpty(ids))
                return string.Empty;
            var configName = new List<string>();
            var config = GetAllList();
            foreach (var item in ids.Split(','))
            {
                var configModel = config.FirstOrDefault(t => t.ID == Convert.ToInt32(item));
                if (configModel != null)
                    configName.Add(configModel.Name);
            }
            return string.Join(",", configName.ToArray());
        }
    }
}
