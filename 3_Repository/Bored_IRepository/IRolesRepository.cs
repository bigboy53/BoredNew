using System;
using System.Collections.Generic;
using Bored.Model;
using DKD.Framework.Data;
using Manage.ViewModel;

namespace Bored.IRepository
{
    public interface IRolesRepository : IDataRepository<Roles>
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        bool Update(RolesDto model);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        int Insert(RolesDto model);
        /// <summary>
        /// 根据角色ID获取权限
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        List<RolePermissionDto> GetPermissionList(int rid);
    }
}
