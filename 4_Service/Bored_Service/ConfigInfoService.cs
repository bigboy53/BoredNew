using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bored.Model;
using Bored.IService;
using Bored.IRepository;
using DKD.Core.Cache;
using DKD.Framework.Const;
using DKD.Framework.Data.Infrastructure;
using Manage.ViewModel;
using PageHelper;

namespace Bored.Service
{
    public class ConfigInfoService:IConfigInfoService
    {
        private readonly IConfigInfoRepository _configInfoDal;

        public ConfigInfoService(IConfigInfoRepository configInfoDal)
        {
            _configInfoDal = configInfoDal;
        }


        public int Add(ConfigInfoDto model)
        {
            var entity = Mapper.Map<ConfigInfo>(model);
            entity.CreateTime = DateTime.Now;
            var result = _configInfoDal.Insert(entity);
            if(result>0)
                CacheManager.Cache.Remove(CacheKey.Config);
            return result;
        }

        public ConfigInfoDto GetModel(int id)
        {
            return GetAllList().FirstOrDefault(t => t.ID == id);
        }

        public PageData GetPage(int page, int rows, int? type,string name)
        {
            var list = GetAllList();
            var pageData = new PageData();
            if (!string.IsNullOrEmpty(name))
                list = list.Where(t => t.Name.Contains(name)).ToList();
            if(type.HasValue)
                list = list.Where(t => t.Type == type.Value).ToList();
            pageData.DataCount = list.Count;
            pageData.Data = list.Skip((page - 1)*rows).Take(rows);
            pageData.PageIndex = page;
            pageData.PageSize = rows;
            return pageData;
        }

        public bool Update(ConfigInfoDto model)
        {
            var oldModel = GetModel(model.ID);
            model.CreateTime = oldModel.CreateTime;
            model.IsDel = oldModel.IsDel;
            var result = _configInfoDal.Update(Mapper.Map<ConfigInfo>(model));
            if(result)
                CacheManager.Cache.Remove(CacheKey.Config);
            return result;
        }

        public bool Delete(string id)
        {
            var idList = id.Split(',');
            var result= _configInfoDal.Update(t => idList.Contains(t.ID.ToString()), t => new ConfigInfo { IsDel = true });
            if(result)
                CacheManager.Cache.Remove(CacheKey.Config);
            return result;
        }

        public List<ConfigInfoDto> GetAllList()
        {
            return _configInfoDal.GetAllList();
        }

        public string GetConfigNames(string ids)
        {
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
