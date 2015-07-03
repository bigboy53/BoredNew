using System;
using System.Collections.Generic;
using AutoMapper;
using Bored.IService;
using Bored.IRepository;
using DKD.Framework.Common;
using DKD.Framework.Data.Infrastructure;
using Manage.ViewModel;
using PageHelper;

namespace Bored.Service
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesDal;

        public RolesService(IRolesRepository rolesDal)
        {
            _rolesDal = rolesDal;
        }

        public IEnumerable<RolesDto> GetList()
        {
            var data= _rolesDal.GetList();
            return Mapper.Map<List<RolesDto>>(data);
        }

        public int Add(RolesDto model)
        {
            model.CreateTime = DateTime.Now;
            model.IsDel = false;
            return _rolesDal.Insert(model);
        }

        public bool Update(RolesDto model)
        {
            model.CreateTime = DateTime.Now;
            return _rolesDal.Update(model);
        }

        public RolesDto GetModel(int id)
        {
            var data = _rolesDal.GetModel(t => t.ID == id);
            return Mapper.Map<RolesDto>(data);
        }


        public PageData GetPage(string name, int page, int rows)
        {
            PageData data;
            if (string.IsNullOrEmpty(name))
                data = _rolesDal.GetPage(page, rows, t => t.CreateTime, OrderType.Desc, null);
            else
                data = _rolesDal.GetPage(page, rows, t => t.CreateTime, OrderType.Desc, t => t.RoleName.Contains(name));
            data.Data = Mapper.Map<List<RolesDto>>(data.Data);
            return data;
        }

        public bool IsExist(string name)
        {
            return _rolesDal.Exist(t => t.RoleName == name);
        }

        public List<RolePermissionDto> GetPermissionList(int rid)
        {
            return _rolesDal.GetPermissionList(rid);
        }
    }
}
