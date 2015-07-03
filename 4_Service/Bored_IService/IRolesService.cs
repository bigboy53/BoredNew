using System.Collections.Generic;
using Manage.ViewModel;
using PageHelper;

namespace Bored.IService
{
    public interface IRolesService
    {
        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<RolesDto> GetList();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(RolesDto model);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(RolesDto model);

        bool IsExist(string name);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RolesDto GetModel(int id);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        PageData GetPage(string name, int page, int rows);
        /// <summary>
        /// 根据角色ID获取权限列表
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        List<RolePermissionDto> GetPermissionList(int rid);

    }
}
