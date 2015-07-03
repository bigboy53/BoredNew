using System;
using System.Collections.Generic;
using Bored.Model;
using Manage.ViewModel;
using PageHelper;

namespace Bored.IService
{
    public interface IManageUsersService
    {
        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<ManageUsersDto> GetList(Func<ManageUsers, bool> where);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(ManageUsersDto model);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(ManageUsersDto model);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ManageUsersDto GetModel(int id);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        PageData GetViewPage(int pageIndex, int pageSize, string name);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Delete(string ids);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="exist">排除名称</param>
        /// <returns></returns>
        bool IsExist(string name,string exist="");

    }
}
