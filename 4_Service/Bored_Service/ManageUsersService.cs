using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bored.Model;
using Bored.IService;
using Bored.IRepository;
using DKD.Framework.Author;
using DKD.Framework.Utility.MD5;
using Manage.ViewModel;
using PageHelper;

namespace Bored.Service
{
    public class ManageUsersService : IManageUsersService
    {
        private readonly IManageUsersRepository _manageUsersDal;

        public ManageUsersService(IManageUsersRepository manageUsersDal)
        {
            _manageUsersDal = manageUsersDal;
        }

        public IEnumerable<ManageUsersDto> GetList(Func<ManageUsers, bool> where)
        {
            var list = _manageUsersDal.GetList(where);
            return Mapper.Map<List<ManageUsersDto>>(list);
        }

        public int Add(ManageUsersDto model)
        {
            var entity = Mapper.Map<ManageUsers>(model);
            entity.IsDel = false;
            entity.CreateTime = DateTime.Now;
            entity.LastLoginTime = DateTime.Now;
            entity.Password = entity.Password.Encrypt();
            return _manageUsersDal.Insert(entity);
        }

        public bool Update(ManageUsersDto model)
        {
            var entity = Mapper.Map<ManageUsers>(model);
            var oldModel = GetModel(entity.ID);
            entity.CreateTime = oldModel.CreateTime;
            entity.LastLoginTime = oldModel.LastLoginTime;
            if (!string.IsNullOrEmpty(entity.Password))
                entity.Password = entity.Password.Encrypt();
            else
                entity.Password = oldModel.Password;
            return _manageUsersDal.Update(entity);
        }

        public ManageUsersDto GetModel(int id)
        {
            var data = _manageUsersDal.GetModel(t => t.ID == id);
            return Mapper.Map<ManageUsersDto>(data);
        }

        public PageData GetViewPage(int pageIndex, int pageSize,string name)
        {
            return _manageUsersDal.GetViewPage(pageIndex, pageSize, name);
        }

        public bool Delete(string ids)
        {
            var idList = ids.Split(',');
            return _manageUsersDal.Update(t => idList.Contains(t.ID.ToString()), t => new ManageUsers { IsDel = true });
        }

        public bool IsExist(string name, string exist = "")
        {
            if (string.IsNullOrEmpty(exist))
                return _manageUsersDal.Exist(t => t.UName == name);
            return _manageUsersDal.Exist(t => t.UName == name && t.UName != exist);
        }

        public bool Login(string userName,string passWord)
        {
            var data = _manageUsersDal.GetModel(t => t.UName == userName && t.Password == passWord.Encrypt());
            if (data != null && data.ID > 0)
            {
                AuthorHelper.SetAuthorInfo(data, true);
                return true;
            }
            return false;
        }
    }
}
